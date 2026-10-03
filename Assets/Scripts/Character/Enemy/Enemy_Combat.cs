using UnityEngine;

public class Enemy_Combat : Entity_Combat
{

    protected override void Awake()
    {
        base.Awake();
    }

    public void Shoot()
    {
        GameObject bullet = weapon.CreateAmmo(attackPoint);
        int damage = weapon.damage;
        float bulletSpeed = weapon.ammoData.speed;
        bullet.GetComponent<AmmoBase>().Setup(bulletSpeed, damage, entity.IsFlipped());
    }
}
