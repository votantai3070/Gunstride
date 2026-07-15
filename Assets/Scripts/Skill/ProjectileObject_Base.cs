using UnityEngine;

public class ProjectileObject_Base : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Collider2D col;

    [SerializeField] protected float speed;
    [SerializeField] protected int damage;

    [SerializeField] protected float attackCooldown = .1f;
    private float lastAttack;
    protected float faceDir;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        rb.linearVelocity = new(faceDir * speed, 0);
    }

    protected virtual void Attack(Collider2D target)
    {
        if (target == null) return;

        if (!CanAttack()) return;
        lastAttack = Time.time;

        IDamageable damageable = target.GetComponent<IDamageable>();

        bool targetHit = damageable.TakeDamage(damage);
        if (targetHit)
        {
            ObjectPool.instance.Despawn(gameObject);
        }
    }

    private bool CanAttack()
    {
        if (Time.time > lastAttack + attackCooldown)
            return true;

        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Attack(collision);
        }
    }
}
