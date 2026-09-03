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

    private bool isPurchased;

    private void Awake()
    {
        shopUI = GetComponentInParent<ShopUI>(true);

        if (weaponImage == null)
            weaponImage = GetComponentInChildren<Image>();

        if (weaponName == null)
            weaponName = GetComponentInChildren<TextMeshProUGUI>();

        if (buyButton == null)
            buyButton = GetComponentInChildren<Button>();
    }

    public void Initialize(WeaponDataSO data)
    {
        weaponData = data;
        weaponImage.sprite = weaponData.weaponSprite;
        weaponImage.SetNativeSize();
        weaponName.text = weaponData.weaponName;
        weaponPrice.text = $"{weaponData.price} coins";

        buyButton.onClick.AddListener(BuyWeapon);
        UpdateBuyButton();
    }

    private void UpdateBuyButton()
    {
        buyButton.interactable = !isPurchased;
        buyButton.GetComponentInChildren<TextMeshProUGUI>().text = isPurchased ? "Purchased" : "Buy";
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

    private void ShowDetailWeapon()
    {
        if (shopUI.DetailWeaponUI != null)
        {
            shopUI.DetailWeaponUI.Initialize(weaponData);
        }
    }

    public bool IsPurchased() => isPurchased;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ShowDetailWeapon();
        }
    }
}
