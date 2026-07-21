using System;
using UnityEngine;

[Serializable]
public class WeightedPrefab
{
    public GameObject prefab;
    [Min(0)] public int weight = 1;
}