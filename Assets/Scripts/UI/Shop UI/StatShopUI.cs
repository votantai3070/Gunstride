using System;
using UnityEngine;

public class StatShopUI : MonoBehaviour, ISaveable
{
    public static Action<string> OnStatDescriptionChanged;

    [SerializeField] private AttributeSlotUI[] attributeSlotUIs;

    private void Awake()
    {
        attributeSlotUIs = GetComponentsInChildren<AttributeSlotUI>(true);
    }

    public void LoadData(GameData data)
    {
        if (data?.statBuffs == null)
            return;

        foreach (AttributeSlotUI slot in attributeSlotUIs)
        {
            if (slot == null)
                continue;

            StatType type = slot.GetStatType();

            if (data.statBuffs.TryGetValue(type, out int savedPoint))
            {
                slot.SetPoint(savedPoint);
            }
            else
            {
                slot.SetPoint(0);
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.statBuffs == null)
        {
            data.statBuffs = new SerializableDictionary<StatType, int>();
        }

        data.statBuffs.Clear();

        foreach (AttributeSlotUI slot in attributeSlotUIs)
        {
            if (slot == null) continue;

            StatType type = slot.GetStatType();
            int point = slot.ExistPoint();

            data.statBuffs[type] = point;
        }
    }

    public AttributeSlotUI[] AttributeSlotUIs()
    {
        return attributeSlotUIs;
    }
}