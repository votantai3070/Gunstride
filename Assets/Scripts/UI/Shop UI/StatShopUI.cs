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
        if (data?.statBuffs == null)
            return;

        Debug.Log($"[StatShopUI] Dictionary count: {data.statBuffs.Count}", this);

        foreach (var pair in data.statBuffs)
        {
            Debug.Log($"[StatShopUI] DATA: {pair.Key} = {pair.Value}", this);
        }

        foreach (AttributeSlotUI slot in attributeSlotUIs)
        {
            if (slot == null)
                continue;

            StatType type = slot.GetStatType();

            if (data.statBuffs.TryGetValue(type, out int savedPoint))
            {
                Debug.Log($"[StatShopUI] LOAD {type} = {savedPoint}", slot);

                slot.SetPoint(savedPoint);
            }
            else
            {
                Debug.LogWarning($"[StatShopUI] No saved point for {type}; set 0.", slot);

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

            Debug.Log($"[StatShopUI] SAVE: {type} = {point}", slot);
        }
    }

    public AttributeSlotUI[] AttributeSlotUIs()
    {
        return attributeSlotUIs;
    }
}