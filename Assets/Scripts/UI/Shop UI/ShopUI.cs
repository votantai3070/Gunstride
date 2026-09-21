using Managers;
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

        // Initialize buttons với data từ weaponListDataSO
        for (int i = 0; i < weaponButtons.Length && i < weaponListDataSO.weaponList.Length; i++)
        {
            weaponButtons[i].Initialize(weaponListDataSO.weaponList[i]);
            weaponButtons[i].gameObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        RefreshWeaponButtons();

        if (selectedWeapon == null && weaponListDataSO.weaponList.Length > 0)
        {
            EquipWeapon(weaponListDataSO.weaponList[0]);
        }
        else if (selectedWeapon != null)
        {
            UpdateEquipButtonUI();
        }

        GameManager.OnCoinChanged += UpdateTotalCoin;
        UpdateTotalCoin(GameManager.Instance.totalCoins);
    }

    private void OnDisable()
    {
        GameManager.OnCoinChanged -= UpdateTotalCoin;
    }

    public void PurchasedWeapons(WeaponDataSO weaponData)
    {
        if (purchasedWeapons.Contains(weaponData))
        {
            Debug.Log($"{weaponData.weaponName} already purchased.");
            return;
        }

        if (!GameManager.Instance.CanSpendCoin(weaponData.price))
        {
            Debug.Log("Insufficient funds!");
            return;
        }

        GameManager.Instance.RemoveCoin(weaponData.price);

        purchasedWeapons.Add(weaponData);

        foreach (var button in weaponButtons)
        {
            if (button.GetWeaponData() == weaponData)
            {
                button.SetIsPurchased(true);
                break;
            }
        }

        Debug.Log($"Purchased {weaponData.weaponName} for {weaponData.price} coins!");
    }

    private void RefreshWeaponButtons()
    {
        foreach (var button in weaponButtons)
        {
            WeaponDataSO weapon = button.GetWeaponData();

            if (weapon == null)
                continue;

            bool isPurchased = purchasedWeapons.Contains(weapon);
            button.SetIsPurchased(isPurchased);
        }

        UpdateEquipButtonUI();
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
        {
            Debug.Log($"Weapon {weaponData.weaponName} is not purchased yet.");
        }
    }

    private void UpdateEquipButtonUI()
    {
        foreach (var button in weaponButtons)
        {
            bool isEquipped = button.GetWeaponData() == selectedWeapon;
            button.SetEquipButtonState(isEquipped);

            if (isEquipped && selectedWeapon != null)
                DetailWeaponUI.Initialize(selectedWeapon);
        }
    }

    private void UpdateTotalCoin(int totalCoin)
    {
        coinTotalText.text = totalCoin.ToString();
    }

    public WeaponDataSO SelectedWeapon() => selectedWeapon;

    public void LoadData(GameData data)
    {
        selectedWeapon = weaponListDataSO.GetWeaponById(data.selectedWeaponId);

        purchasedWeapons.Clear();
        foreach (var weapon in data.weaponPurchased)
        {
            WeaponDataSO weaponData = weapon.Value;

            if (!purchasedWeapons.Contains(weaponData))
                purchasedWeapons.Add(weaponData);
        }

        // Auto-purchase weapons giá 0
        foreach (var weapon in weaponListDataSO.weaponList)
        {
            if (weapon.price == 0 && !purchasedWeapons.Contains(weapon))
            {
                purchasedWeapons.Add(weapon);
            }
        }

        RefreshWeaponButtons();
    }

    public void SaveData(ref GameData data)
    {
        data.selectedWeaponId = selectedWeapon != null ? selectedWeapon.weaponID : string.Empty;
        data.weaponPurchased.Clear();

        foreach (var weapon in purchasedWeapons)
        {
            data.weaponPurchased[weapon.weaponID] = weapon;
        }
    }
}