using TMPro;
using UnityEngine;

public class DetailWeaponUI : MonoBehaviour
{
    [SerializeField] private WeaponDataSO weaponData;

    [Header("Detail Weapon References")]
    [SerializeField] private GameObject weaponName;
    [SerializeField] private GameObject weaponBullet;
    [SerializeField] private GameObject weaponDamage;
    [SerializeField] private GameObject weaponFireRate;
    [SerializeField] private GameObject weaponPrice;
    [SerializeField] private GameObject weaponDescription;

    private void OnDisable()
    {
        Initialize(null);
    }

    public void Initialize(WeaponDataSO weaponData)
    {
        if (weaponData == null)
        {
            weaponName.GetComponentInChildren<TextMeshProUGUI>().text = "";
            weaponBullet.GetComponentInChildren<TextMeshProUGUI>().text = "";
            weaponFireRate.GetComponentInChildren<TextMeshProUGUI>().text = "";
            weaponDamage.GetComponentInChildren<TextMeshProUGUI>().text = "";
            weaponPrice.GetComponentInChildren<TextMeshProUGUI>().text = "";
            weaponDescription.GetComponentInChildren<TextMeshProUGUI>().text = "";
            SetActiveDetail(false);
            return;
        }

        this.weaponData = weaponData;
        weaponName.GetComponentInChildren<TextMeshProUGUI>().text = weaponData.weaponName;
        weaponBullet.GetComponentInChildren<TextMeshProUGUI>().text = FormatBullet(weaponData.ammoData.ammoType);
        weaponFireRate.GetComponentInChildren<TextMeshProUGUI>().text = FormatFireRate(weaponData.fireRate);
        weaponDamage.GetComponentInChildren<TextMeshProUGUI>().text = FormatDamage(weaponData.damage);
        weaponPrice.GetComponentInChildren<TextMeshProUGUI>().text = $"{weaponData.price} coins";
        weaponDescription.GetComponentInChildren<TextMeshProUGUI>().text = weaponData.weaponDescription;
        SetActiveDetail(true);
    }

    private void SetActiveDetail(bool active)
    {
        weaponName.SetActive(active);
        weaponBullet.SetActive(active);
        weaponDamage.SetActive(active);
        weaponPrice.SetActive(active);
        weaponDescription.SetActive(active);
        weaponFireRate.SetActive(active);
    }

    private string FormatDamage(int damage) => damage.ToString() + " HP";

    private string FormatFireRate(float fireRate) => fireRate.ToString() + " shots/s";

    private string FormatBullet(AmmoType ammoType)
    {
        return ammoType switch
        {
            AmmoType.NineMm => "9mm",
            AmmoType.ThreeFiveSevenMagnum => "357 Magnum",
            AmmoType.Rocket => "Rocket",
            AmmoType.CrossbowArrow => "Crossbow Arrow",
            AmmoType.TwelveGauge => "12 Gauge",
            AmmoType.FiveFiveSixMm => "5.56mm",
            AmmoType.SevenSixTwoMm => "7.62mm",
            AmmoType.Arrow => "Arrow",
            _ => "Unknown",
        };
    }
}

