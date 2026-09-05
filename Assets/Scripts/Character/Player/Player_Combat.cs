using UnityEngine;

public class Player_Combat : Entity_Combat
{
    private Player player;
    private Animator weaponAnim;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<Player>();
    }

    protected override void Start()
    {
        base.Start();

        ObjectPool.Instance.Spawn(weapon.weaponName, equipWeaponPoint.position, Quaternion.identity, equipWeaponPoint);
        weaponAnim = GetComponentInChildren<WeaponBase>().weaponAnimator;
    }

    private void Update()
    {
        if (weapon.CanShoot() && player.CanAttackTarget(weapon.range))
        {
            weaponAnim.SetTrigger("Attack");
        }
    }

    public void Shoot(Transform attackPoint)
    {
        GameObject bullet = weapon.CreateAmmo(attackPoint);
        bullet.GetComponent<AmmoBase>().Setup(weapon.ammoData.speed, weapon.damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.right * weapon.range);
    }
}