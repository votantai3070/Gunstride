using UnityEngine;

public class ProjectileObject_Base : MonoBehaviour
{
    protected Utils utils = new Utils();
    protected Rigidbody2D rb;
    protected Collider2D col;

    [SerializeField] protected float speed;
    [SerializeField] protected int damage;

    [SerializeField] protected float attackCooldown = .1f;
    protected float lastAttack;
    protected float faceDir;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void Start()
    {
        if (faceDir == -1)
            utils.FlipLeft(transform);
    }

    protected virtual void OnEnable()
    {
        lastAttack = -999f;
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
        if (damageable == null) return;

        bool targetHit = damageable.TakeDamage(damage);
        if (targetHit)
        {
            ObjectPool.instance.Despawn(gameObject);

            VFX_AutomationEffect vfx = GetComponent<VFX_AutomationEffect>();
            if (vfx != null)
                vfx.CreateEffect(target.transform);
        }
    }

    protected bool CanAttack()
    {
        return Time.time > lastAttack + attackCooldown;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Attack(collision);
    }
}