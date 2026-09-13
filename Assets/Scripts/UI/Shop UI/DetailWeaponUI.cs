using TMPro;
using UnityEngine;

public class DetailWeaponUI : MonoBehaviour
{
    [SerializeField] private WeaponDataSO weaponData;

    [Header("Detail Weapon References")]
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private TextMeshProUGUI weaponBullet;
    [SerializeField] private TextMeshProUGUI weaponDamage;
    [SerializeField] private TextMeshProUGUI weaponFireRate;
    [SerializeField] private TextMeshProUGUI weaponPrice;
    [SerializeField] private TextMeshProUGUI weaponDescription;

    private void OnDisable()
    {
        Initialize(null);
    }

    public void Initialize(WeaponDataSO weaponData)
    {
        if (weaponData == null)
        {
            weaponName.text = "";
            weaponBullet.text = "";
            weaponFireRate.text = "";
            weaponDamage.text = "";
            weaponPrice.text = "";
            weaponDescription.text = "";
            return;
        }

        this.weaponData = weaponData;
        weaponName.text = weaponData.weaponName;
        weaponBullet.text = FormatBullet(weaponData.ammoData.ammoType);
        weaponFireRate.text = FormatFireRate(weaponData.fireRate);
        weaponDamage.text = FormatDamage(weaponData.damage);
        weaponPrice.text = $"{weaponData.price} coins";
        weaponDescription.text = weaponData.weaponDescription;
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

