using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity entity;

    [Header("Attack Settings")]
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private LayerMask attackLayer;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackCooldown = 0.25f;

    private float lastAttackTime;

    private void Awake()
    {
        entity = GetComponent<Entity>();
    }

    public void TryAttack(GameObject target)
    {
        if (Time.time < lastAttackTime + attackCooldown)
            return;

        lastAttackTime = Time.time;
        Attack(target);
    }

    private void Attack(GameObject target)
    {
        if (target != null)
        {
            IDamageable damageable = target.GetComponent<IDamageable>();
            damageable?.TakeDamage(attackDamage);
        }

        Debug.Log("Attack");
    }
}
