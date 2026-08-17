using System.Collections.Generic;
using UnityEngine;

public class StatusIconBarUI : MonoBehaviour
{
    [SerializeField] private StatusIconSlotUI slotPrefab;
    [SerializeField] private StatusIconSlotUI enemySlotPrefab;
    [SerializeField] private Transform slotParent;

    private readonly List<StatusIconSlotUI> activeSlots = new();

    public void AddOrRefreshEffect(string id, Sprite icon, float duration, Entity slotParent, int stack = 1)
    {
        StatusIconSlotUI existingSlot = FindSlot(id);

        if (existingSlot != null)
        {
            StatusEffectUIData data = new()
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

        StatusIconSlotUI slotUI = slotParent is Enemy ? enemySlotPrefab : slotPrefab;

        StatusIconSlotUI newSlot = ObjectPool.Instance.Spawn
            (slotUI.name, transform.position, Quaternion.identity, this.slotParent)
            .GetComponent<StatusIconSlotUI>();

        StatusEffectUIData newData = new()
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