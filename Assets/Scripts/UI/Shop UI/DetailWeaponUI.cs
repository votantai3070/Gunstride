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

    public void Initialize(WeaponDataSO weaponData)
    {
        this.weaponData = weaponData;
        weaponName.text = weaponData.weaponName;
        weaponBullet.text = FormatBullet(weaponData.ammoData.ammoType);
        weaponFireRate.text = FormatFireRate(weaponData.fireRate);
        weaponDamage.text = FormatDamage(weaponData.damage);
        weaponPrice.text = $"{weaponData.price} coins";
    }

    private string FormatDamage(int damage) => damage.ToString() + " HP";

    private string FormatFireRate(float fireRate) => fireRate.ToString() + " shots/s";

    private string FormatBullet(AmmoType ammoType)
    {
        return ammoType switch
        {
            AmmoType.NineMm => "9mm",
            AmmoType.TwelveGauge => "12 Gauge",
            AmmoType.FiveFiveSixMm => "5.56mm",
            AmmoType.SevenSixTwoMm => "7.62mm",
            AmmoType.Arrow => "Arrow",
            _ => "Unknown",
        };
    }
}

