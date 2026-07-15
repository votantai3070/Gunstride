using UnityEngine;

public class ProjectileObject_Base : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Collider2D col;

    [SerializeField] protected float speed;
    [SerializeField] protected int damage;

    [SerializeField] protected float attackCooldown = .1f;
    private float lastAttack;
    protected bool faceLeftDir;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        rb.linearVelocity = new(faceLeftDir ? -1 : 1 * speed, 0);
    }

    protected virtual void Attack(Collider2D target)
    {
        if (target == null) return;

        lastAttack = Time.time;

        if (!CanAttack()) return;

        if (target.TryGetComponent(out IDamageable damageable))
        {

            //bool targetHit = damageable?.TakeDamage(damage);
        }
    }

    private bool CanAttack()
    {
        if (Time.time > lastAttack + attackCooldown)
            return true;

        return false;
    }
}
