using System.Collections.Generic;
using UnityEngine;

public class StatusIconBarUI : MonoBehaviour
{
    [SerializeField] private StatusIconSlotUI slotPrefab;
    [SerializeField] private Transform slotParent;

    private readonly List<StatusIconSlotUI> activeSlots = new();

    public void AddOrRefreshEffect(string id, Sprite icon, float duration, int stack = 1)
    {
        StatusIconSlotUI existingSlot = FindSlot(id);

        if (existingSlot != null)
        {
            StatusEffectUIData data = new StatusEffectUIData
            {
                id = id,
                icon = icon,
                duration = duration,
                remainingTime = duration,
                stack = stack
            };

            existingSlot.SetEffect(data);
            return;
        }

        StatusIconSlotUI newSlot = ObjectPool.Instance.Spawn
            (slotPrefab.name, transform.position, Quaternion.identity, slotParent.transform)
            .GetComponent<StatusIconSlotUI>();

        StatusEffectUIData newData = new StatusEffectUIData
        {
            id = id,
            icon = icon,
            duration = duration,
            remainingTime = duration,
            stack = stack
        };

        newSlot.SetEffect(newData);
        activeSlots.Add(newSlot);
    }

    private StatusIconSlotUI FindSlot(string id)
    {
        foreach (StatusIconSlotUI slot in activeSlots)
        {
            if (slot.EffectId == id)
                return slot;
        }

        return null;
    }
}