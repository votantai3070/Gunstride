using UnityEngine;

public class StatShopUI : MonoBehaviour, ISaveable
{
    [SerializeField] private AttributeSlotUI[] attributeSlotUIs;

    private void Awake()
    {
        attributeSlotUIs = GetComponentsInChildren<AttributeSlotUI>(true);
    }

    public void LoadData(GameData data)
    {
        if (data == null)
            return;

        if (data.statBuffs == null)
        {
            Debug.LogWarning("[StatShopUI] statBuffs is null.", this);
            return;
        }

        foreach (AttributeSlotUI slot in attributeSlotUIs)
        {
            if (slot == null)
                continue;

            if (data.statBuffs.TryGetValue(slot.GetStatType(), out int savedPoint))
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
            if (slot == null)
                continue;

            data.statBuffs[slot.GetStatType()] = slot.ExistPoint();
        }
    }

    public AttributeSlotUI[] AttributeSlotUIs()
    {
        return attributeSlotUIs;
    }
}