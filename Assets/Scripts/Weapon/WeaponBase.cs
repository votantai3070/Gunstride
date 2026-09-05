using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public Animator weaponAnimator { get; private set; }
    public AttackPoint attackPoint { get; private set; }

    private void Awake()
    {
        weaponAnimator = GetComponentInChildren<Animator>();
        attackPoint = GetComponentInChildren<AttackPoint>();
    }
}
