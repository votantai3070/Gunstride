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

        switchButtons = GetComponentsInChildren<SwitchButton>(true);
    }

    private void Start()
    {
        SwitchToShopList();
    }

    public void SwitchToShopList()
    {
        shopList.SetActive(true);
        statList.SetActive(false);

        if (ui != null && ui.ShopUI.SelectedWeapon() != null)
            ui.ShopUI.DetailWeaponUI.Initialize(ui.ShopUI.SelectedWeapon());

        UpdateVisualButton();
    }

    public void SwitchToStatList()
    {
        shopList.SetActive(false);
        statList.SetActive(true);

        if (ui != null)
            ui.ShopUI.DetailWeaponUI.Initialize(null);

        UpdateVisualButton();
    }

    public void UpdateVisualButton()
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