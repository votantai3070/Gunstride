using UnityEngine;

public class AmmoBase : MonoBehaviour
{
    [SerializeField] protected int damage = 1;
    [SerializeField] private float lifetime = 5f;
    [SerializeField] protected bool isCrit;

    private void OnEnable()
    {
        lifetime = 5f; // Reset lifetime when the bullet is enabled
    }

    protected virtual void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0f)
        {
            AutomaticDespawnObject();
        }
    }

    public void Setup(float bulletSpeed, int bulletDamage, bool isCrit = false)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * bulletSpeed;
        damage = bulletDamage;
        this.isCrit = isCrit;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!collision.TryGetComponent(out IDamageable damageable))
                return;

            damageable.TakeDamage(damage, isCrit);
            AutomaticDespawnObject();
        }
    }

    protected void AutomaticDespawnObject()
    {
        ObjectPool.Instance.Despawn(gameObject);
    }
}
