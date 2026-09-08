using UnityEngine;

public class RocketAmmo : AmmoBase
{
    [Header("Rocket Settings")]
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private LayerMask damageableLayers;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Explode();
            AutomaticDespawnObject();
        }
    }

    private void Explode()
    {
        Debug.Log("Enemies Detected: " + GetTarget().Length);

        foreach (var target in GetTarget())
        {
            if (!target.TryGetComponent(out IDamageable damageable))
                continue;

            damageable.TakeDamage(damage);
        }
    }

    private Collider2D[] GetTarget()
    {
        return Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageableLayers);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
