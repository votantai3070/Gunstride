using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour, ISaveable
{
    public DetailWeaponUI DetailWeaponUI { get; private set; }

    [SerializeField] private List<WeaponDataSO> purchasedWeapons = new();
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

        if (weaponListDataSO == null)
        {
            Debug.LogError(
                "[ShopUI] weaponListDataSO is not assigned.",
                this
            );
            return;
        }

        if (DetailWeaponUI == null)
        {
            Debug.LogError(
                "[ShopUI] DetailWeaponUI was not found.",
                this
            );
        }

        for (int i = 0;
             i < weaponButtons.Length &&
             i < weaponListDataSO.weaponList.Length;
             i++)
        {
            weaponButtons[i].Initialize(
                weaponListDataSO.weaponList[i]
            );

            weaponButtons[i].gameObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        if (CoinManager.Instance != null)
        {
            CoinManager.OnCoinChanged += UpdateTotalCoin;
            UpdateTotalCoin(CoinManager.Instance.totalCoins);
        }

        RefreshWeaponButtons();

        if (selectedWeapon == null &&
            weaponListDataSO != null &&
            weaponListDataSO.weaponList.Length > 0)
        {
            EquipWeapon(weaponListDataSO.weaponList[0]);
        }
        else if (selectedWeapon != null)
        {
            UpdateEquipButtonUI();
        }
    }

    private void OnDisable()
    {
        CoinManager.OnCoinChanged -= UpdateTotalCoin;
    }

    public void PurchasedWeapons(WeaponDataSO weaponData)
    {
        if (weaponData == null)
            return;

        if (purchasedWeapons.Contains(weaponData))
            return;

        if (CoinManager.Instance == null ||
            !CoinManager.Instance.CanSpendCoin(weaponData.price))
        {
            Debug.Log("Insufficient funds.");
            return;
        }

        // Chỉ đổi dữ liệu trong RAM.
        CoinManager.Instance.RemoveCoin(weaponData.price);
        purchasedWeapons.Add(weaponData);

        RefreshWeaponButtons();

        // Save sau khi cả coin và weapon đã cập nhật.
        SaveManager.instance.SaveGame();
    }

    public void EquipWeapon(WeaponDataSO weaponData)
    {
        if (weaponData == null ||
            !purchasedWeapons.Contains(weaponData))
        {
            return;
        }

        selectedWeapon = weaponData;
        UpdateEquipButtonUI();
        SaveManager.instance.SaveGame();

        // Nếu equip weapon cũng cần lưu, mở comment:
        // SaveManager.instance.SaveGame();
    }

    private void RefreshWeaponButtons()
    {
        if (weaponButtons == null)
            return;

        foreach (WeaponButtonUI button in weaponButtons)
        {
            if (button == null)
                continue;

            WeaponDataSO weapon = button.GetWeaponData();

            if (weapon == null)
                continue;

            button.SetIsPurchased(
                purchasedWeapons.Contains(weapon)
            );
        }

        UpdateEquipButtonUI();
    }

    private void UpdateEquipButtonUI()
    {
        if (weaponButtons == null)
            return;

        foreach (WeaponButtonUI button in weaponButtons)
        {
            if (button == null)
                continue;

            bool isEquipped =
                button.GetWeaponData() == selectedWeapon;

            button.SetEquipButtonState(isEquipped);

            if (isEquipped &&
                selectedWeapon != null &&
                DetailWeaponUI != null)
            {
                DetailWeaponUI.Initialize(selectedWeapon);
            }
        }
    }

    private void UpdateTotalCoin(int totalCoin)
    {
        if (coinTotalText != null)
            coinTotalText.text = totalCoin.ToString();
    }

    public WeaponDataSO SelectedWeapon()
    {
        return selectedWeapon;
    }

    public void LoadData(GameData data)
    {
        if (data == null || weaponListDataSO == null)
            return;

        selectedWeapon = weaponListDataSO.GetWeaponById(
            data.selectedWeaponId
        );

        purchasedWeapons.Clear();

        if (data.weaponPurchased != null)
        {
            foreach (var pair in data.weaponPurchased)
            {
                WeaponDataSO weaponData = pair.Value;

                if (weaponData != null &&
                    !purchasedWeapons.Contains(weaponData))
                {
                    purchasedWeapons.Add(weaponData);
                }
            }
        }

        foreach (WeaponDataSO weapon in weaponListDataSO.weaponList)
        {
            if (weapon != null && weapon.price == 0 && !purchasedWeapons.Contains(weapon))
            {
                purchasedWeapons.Add(weapon);
            }
        }

        RefreshWeaponButtons();
    }

    public void SaveData(ref GameData data)
    {
        if (data == null)
            return;

        if (data.weaponPurchased == null)
        {
            data.weaponPurchased = new SerializableDictionary<string, WeaponDataSO>();
        }

        data.selectedWeaponId = selectedWeapon != null ? selectedWeapon.weaponID : string.Empty;

        data.weaponPurchased.Clear();

        foreach (WeaponDataSO weapon in purchasedWeapons)
        {
            if (weapon == null)
                continue;

            data.weaponPurchased[weapon.weaponID] = weapon;
        }
    }
}