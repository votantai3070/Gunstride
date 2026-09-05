using UnityEngine;

[System.Serializable]
public class Weapon
{
    [Header("Weapon Stats")]
    public GameObject weaponPrefab;
    public string weaponName;
    public int damage;
    public float range;
    public float fireRate;
    public int price;

    [Header("Ammo Data")]
    public AmmoData ammoData;

    private float lastShootTime;

    public Weapon(WeaponDataSO weaponDataSO)
    {
        weaponPrefab = weaponDataSO.weaponPrefab;
        weaponName = weaponDataSO.weaponName;
        damage = weaponDataSO.damage;
        range = weaponDataSO.weaponRange;
        fireRate = weaponDataSO.fireRate;
        price = weaponDataSO.price;

        ammoData = weaponDataSO.ammoData;
    }

    public bool CanShoot()
    {
        if (ReadyToFire())
        {
            return true;
        }

        return false;
    }

    private bool ReadyToFire()
    {
        if (Time.time > lastShootTime + 1 / fireRate)
        {
            lastShootTime = Time.time;
            return true;
        }

        return false;
    }

    public GameObject CreateAmmo(Transform transform)
    {
        return ObjectPool.Instance.Spawn(ammoData.ammoType.ToString(), transform.position, transform.rotation);
    }
}
