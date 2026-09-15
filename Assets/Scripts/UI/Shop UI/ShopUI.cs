using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour, ISaveable
{
    public DetailWeaponUI DetailWeaponUI { get; private set; }

    [SerializeField] private List<WeaponDataSO> purchasedWeapons = new List<WeaponDataSO>();
    [SerializeField] private WeaponDataSO selectedWeapon;

    [Header("Available Weapons")]
    [SerializeField] private Weapon_ListDataSO weaponListDataSO;
    private WeaponButtonUI[] weaponButtons;

    [Header("Coin UI")]
    [SerializeField] private TextMeshProUGUI coinTotalText;

    private void Awake()
    {
        weaponButtons = GetComponentsInChildren<WeaponButtonUI>(true);
        DetailWeaponUI = GetComponentInChildren<DetailWeaponUI>(true);
        ShowWeaponList();
    }


    private void Start()
    {
        // Automatically purchase weapons with a price of 0
        foreach (var weapon in weaponListDataSO.weaponList)
        {
            if (weapon.price == 0)
                PurchasedWeapons(weapon);
        }
    }

    private void OnEnable()
    {
        if (selectedWeapon != null)
            EquipWeapon(selectedWeapon);
        else
            EquipWeapon(weaponListDataSO.weaponList[0]); // Equip the first weapon by default
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
    private void ShowWeaponList()
    {
        for (int i = 0; i < weaponButtons.Length && i < weaponListDataSO.weaponList.Length; i++)
        {
            weaponButtons[i].Initialize(weaponListDataSO.weaponList[i]);
            weaponButtons[i].gameObject.SetActive(true);
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
            bool isEquipped = button.GetWeaponData() == selectedWeapon;
            button.SetEquipButtonState(isEquipped);

            if (isEquipped)
                DetailWeaponUI.Initialize(selectedWeapon);
        }
    }

    public void LoadData(GameData data)
    {
        selectedWeapon = weaponListDataSO.GetWeaponById(data.selectedWeaponId);
        coinTotalText.text = data.coins.ToString();
    }

    public void SaveData(ref GameData data)
    {
        data.selectedWeaponId = selectedWeapon != null ? selectedWeapon.weaponID : string.Empty;
    }
}
