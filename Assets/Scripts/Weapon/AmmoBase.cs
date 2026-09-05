using UnityEngine;

public class AmmoBase : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifetime = 5f;

    private void OnEnable()
    {
        lifetime = 5f; // Reset lifetime when the bullet is enabled
    }

    private void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0f)
        {
            AutomaticDespawnObject();
        }
    }

    public void Setup(float bulletSpeed, int bulletDamage)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * bulletSpeed;
        damage = bulletDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!collision.TryGetComponent(out IDamageable damageable))
                return;

            damageable.TakeDamage(damage);
            AutomaticDespawnObject();
        }
    }

    private void AutomaticDespawnObject()
    {
        ObjectPool.Instance.Despawn(gameObject);
    }
}
