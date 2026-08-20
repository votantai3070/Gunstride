using UnityEngine;

public class ExplodeUpgrade : MonoBehaviour, IProjectileUpgrade
{
    [Header("Explode Upgrade")]
    [SerializeField] private float explosionRadius = 1.5f;
    [SerializeField] private int explosionDamage = 1;
    [SerializeField] private LayerMask targetMask;

    private ProjectileObject_Base projectile;
    private bool hasExploded;

    public SkillUpgradeType upgradeType => SkillUpgradeType.Explode;
    public bool ShouldDespawn => true;

    public void Initialize(ProjectileObject_Base projectile, SkillBuffDataSO skillBuffData)
    {
        this.projectile = projectile;
        hasExploded = false;

        if (skillBuffData is ItemBuff_Explode explodeData)
        {
            explosionRadius = explodeData.explosionRadius;
            explosionDamage = explodeData.explosionDamage;
            targetMask = explodeData.explodeTargetMask;
            Debug.Log($"ExplodeUpgrade: Initialized with radius {explosionRadius}, Damage {explosionDamage}");
        }
    }

    public void OnHit(Collider2D target)
    {
        if (hasExploded)
            return;

        hasExploded = true;

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            projectile.transform.position,
            explosionRadius,
            targetMask
        );

        foreach (Collider2D hit in hits)
        {
            if (hit == null || hit == target)
                continue;

            IDamageable damageable = hit.GetComponent<IDamageable>();
            if (damageable == null)
                continue;

            damageable.TakeDamage(explosionDamage);
        }
    }
}