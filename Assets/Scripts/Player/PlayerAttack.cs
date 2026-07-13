using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private LayerMask attackLayer;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackCooldown = 0.25f;

    private float lastAttackTime;

    public void TryAttack()
    {
        if (Time.time < lastAttackTime + attackCooldown)
            return;

        lastAttackTime = Time.time;
        Attack();
    }

    private void Attack()
    {
        Vector2 origin = transform.position;
        Vector2 direction = Vector2.right;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, attackRange, attackLayer);

        if (hit.collider != null)
        {
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);
            }

            Debug.DrawRay(origin, direction * attackRange, Color.red, 0.2f);
        }
        else
        {
            Debug.DrawRay(origin, direction * attackRange, Color.yellow, 0.2f);
        }

        Debug.Log("Attack");
    }
}