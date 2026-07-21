using System.Collections.Generic;
using UnityEngine;

public static class WeightedRandomUtility
{
    public static GameObject PickPrefab(List<WeightedPrefab> items)
    {
        if (items == null || items.Count == 0)
            return null;

        int totalWeight = 0;

        for (int i = 0; i < items.Count; i++)
        {
            WeightedPrefab item = items[i];

            if (item == null || item.prefab == null)
                continue;

            if (item.weight > 0)
                totalWeight += item.weight;
        }

        if (totalWeight <= 0)
            return null;

        int roll = Random.Range(0, totalWeight);

        for (int i = 0; i < items.Count; i++)
        {
            WeightedPrefab item = items[i];

            if (item == null || item.prefab == null || item.weight <= 0)
                continue;

            if (roll < item.weight)
                return item.prefab;

            roll -= item.weight;
        }

        return null;
    }
}