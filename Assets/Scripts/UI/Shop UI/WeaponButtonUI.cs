using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponButtonUI : MonoBehaviour, IPointerClickHandler
{
    private ShopUI shopUI;
    [SerializeField] private WeaponDataSO weaponData;

    [Header("UI References")]
    [SerializeField] private Image weaponImage;
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private TextMeshProUGUI weaponPrice;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button equipButton;
    [SerializeField] private Image fadeLockImage;

    private bool isPurchased;

    private void Awake()
    {
        shopUI = GetComponentInParent<ShopUI>(true);
    }

    public void Initialize(WeaponDataSO data)
    {
        weaponData = data;
        weaponImage.sprite = weaponData.weaponSprite;
        weaponImage.SetNativeSize();
        weaponName.text = weaponData.weaponName;
        weaponPrice.text = $"{weaponData.price} coins";

        equipButton.onClick.AddListener(() => shopUI.EquipWeapon(weaponData));
        buyButton.onClick.AddListener(BuyWeapon);
        UpdateBuyButton();
    }

    private void UpdateBuyButton()
    {
        if (isPurchased)
        {
            buyButton.interactable = false;

            buyButton.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(true);

            fadeLockImage.gameObject.SetActive(false);
        }
        else
        {
            buyButton.interactable = true;

            buyButton.gameObject.SetActive(true);
            equipButton.gameObject.SetActive(false);

            fadeLockImage.gameObject.SetActive(true);
        }
    }

    public void BuyWeapon()
    {
        if (!isPurchased)
        {
            isPurchased = true;
            shopUI.PurchasedWeapons(weaponData);
            UpdateBuyButton();
            Debug.Log($"Weapon {weaponData.weaponName} purchased!");
        }
        else
        {
            Debug.Log($"Weapon {weaponData.weaponName} is already purchased.");
        }
    }

    public void ShowDetailWeapon()
    {
        if (shopUI.DetailWeaponUI != null)
        {
            shopUI.DetailWeaponUI.Initialize(weaponData);
        }
    }

    public void SetEquipButtonState(bool isEquipped)
    {
        equipButton.interactable = !isEquipped;
        equipButton.GetComponentInChildren<TextMeshProUGUI>().text = isEquipped ? "Equipped" : "Equip";
    }

    public void SetIsPurchased(bool purchased)
    {
        isPurchased = purchased;
        UpdateBuyButton();
    }

    public bool IsPurchased() => isPurchased;
    public WeaponDataSO GetWeaponData() => weaponData;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ShowDetailWeapon();
        }
    }
}
