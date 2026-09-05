using UnityEngine;

public class Weapon_AnimationEvents : MonoBehaviour
{
    public void Shoot()
    {
        Player player = GetComponentInParent<Player>();
        if (player != null)
        {
            AttackPoint attackPoint = GetComponentInParent<WeaponBase>().attackPoint;
            player.combat.Shoot(attackPoint.transform);
        }
    }
}
