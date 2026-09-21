using UnityEngine;

public class SwitchContainer : MonoBehaviour
{
    private UI ui;
    [SerializeField] private GameObject shopList;
    [SerializeField] private GameObject statList;

    [SerializeField] private SwitchButton[] switchButtons;

    private void Awake()
    {
        ui = GetComponentInParent<UI>();
        if (switchButtons == null || switchButtons.Length == 0)
            switchButtons = GetComponentsInChildren<SwitchButton>(true);

        SwitchToShopList();
    }

    public void SwitchToShopList()
    {
        shopList.SetActive(true);
        statList.SetActive(false);

        if (ui != null && ui.ShopUI.SelectedWeapon() != null)
            ui.ShopUI.DetailWeaponUI.Initialize(ui.ShopUI.SelectedWeapon());

        SwitchTo();
    }

    public void SwitchToStatList()
    {
        shopList.SetActive(false);
        statList.SetActive(true);

        if (ui != null)
            ui.ShopUI.DetailWeaponUI.Initialize(null);

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