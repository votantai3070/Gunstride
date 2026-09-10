using UnityEngine;

public class RocketAmmo : AmmoBase
{
    [Header("Rocket Settings")]
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private LayerMask damageableLayers;
    [SerializeField] private GameObject explosionEffectPrefab;

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
        if (explosionEffectPrefab != null)
        {
            GameObject explosion = ObjectPool.Instance.Spawn(explosionEffectPrefab.name, transform.position, Quaternion.identity);
            explosion.GetComponent<ShockwaveDamage2D>().SetupExplode(damage, damageableLayers);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
