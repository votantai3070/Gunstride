using UnityEngine;

public class SwitchContainer : MonoBehaviour
{
    [SerializeField] private GameObject shopList;
    [SerializeField] private GameObject statList;

    [SerializeField] private SwitchButton[] switchButtons;

    private void Awake()
    {
        if (switchButtons == null || switchButtons.Length == 0)
            switchButtons = GetComponentsInChildren<SwitchButton>(true);

        SwitchToShopList();
    }

    public void SwitchToShopList()
    {
        shopList.SetActive(true);
        statList.SetActive(false);

        foreach (var btn in switchButtons)
        {
            bool isShop = btn.GetType().GetField("tabType",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance) != null;
        }

        SwitchTo();
    }

    public void SwitchToStatList()
    {
        shopList.SetActive(false);
        statList.SetActive(true);

        SwitchTo();
    }

    public void SwitchTo()
    {
        foreach (var btn in switchButtons)
        {
            bool isActive = false;

            if (shopList.activeSelf && btn.GetTabType() == SwitchButton.TabType.Shop)
                isActive = true;
            else if (statList.activeSelf && btn.GetTabType() == SwitchButton.TabType.Stat)
                isActive = true;

            btn.SetActiveVisual(isActive);
        }
    }
}