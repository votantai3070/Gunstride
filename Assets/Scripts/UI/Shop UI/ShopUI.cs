using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public DetailWeaponUI DetailWeaponUI { get; private set; }

    [SerializeField] private List<WeaponDataSO> purchasedWeapons = new List<WeaponDataSO>();
    [SerializeField] private WeaponDataSO selectedWeapon;

    [Header("Available Weapons")]
    [SerializeField] private Weapon_ListDataSO weaponListDataSO;
    private WeaponButtonUI[] weaponButtons;

    private void Awake()
    {
        weaponButtons = GetComponentsInChildren<WeaponButtonUI>(true);
        DetailWeaponUI = GetComponentInChildren<DetailWeaponUI>(true);
    }

    private void Start()
    {
        for (int i = 0; i < weaponButtons.Length && i < weaponListDataSO.weaponList.Length; i++)
        {
            weaponButtons[i].Initialize(weaponListDataSO.weaponList[i]);
            weaponButtons[i].gameObject.SetActive(true);
        }

        // Automatically purchase weapons with a price of 0
        foreach (var weapon in weaponListDataSO.weaponList)
        {
            if (weapon.price == 0)
                PurchasedWeapons(weapon);
        }
    }

    public void PurchasedWeapons(WeaponDataSO weaponData)
    {
        if (!purchasedWeapons.Contains(weaponData))
            purchasedWeapons.Add(weaponData);

        foreach (var button in weaponButtons)
        {
            if (button.GetWeaponData() == weaponData)
            {
                button.SetIsPurchased(true);
                break;
            }
        }
    }

    public void EquipWeapon(WeaponDataSO weaponData)
    {
        if (purchasedWeapons.Contains(weaponData))
        {
            selectedWeapon = weaponData;
            UpdateEquipButtonUI();
            Debug.Log($"Weapon {weaponData.weaponName} equipped!");
        }
        else
            Debug.Log($"Weapon {weaponData.weaponName} is not purchased yet.");
    }

    private void UpdateEquipButtonUI()
    {
        foreach (var button in weaponButtons)
        {
            if (button != null && button.gameObject.activeSelf)
            {
                bool isEquipped = button.GetWeaponData() == selectedWeapon;
                button.SetEquipButtonState(isEquipped);
                //button.ShowDetailWeapon();
            }
        }
    }
}
