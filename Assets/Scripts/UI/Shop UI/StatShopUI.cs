using UnityEngine;

public class StatShopUI : MonoBehaviour
{
    [SerializeField] private AttributeSlotUI[] attributeSlotUIs;

    private void Awake()
    {
        attributeSlotUIs = GetComponentsInChildren<AttributeSlotUI>(true);
    }
}
