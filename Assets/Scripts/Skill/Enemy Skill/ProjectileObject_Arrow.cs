using System.Collections.Generic;
using UnityEngine;

public class ProjectileObject_Arrow : ProjectileObject_Base
{
    private Projectile_Arrow arrowManager;

    [Header("Arrow Runtime")]
    [SerializeField] private int remainingPierce;
    [SerializeField] private float explodeRadius;
    [SerializeField] private int explodeDamage;
    [SerializeField] private LayerMask explodeTargetMask;

    private bool canPierce;
    private bool canExplode;
    private bool canSingle;
    private bool hasExploded;

    private readonly HashSet<Collider2D> hitTargets = new();

    public void SetupArrow(Projectile_Arrow arrowManager)
    {
        this.arrowManager = arrowManager;

        damage = arrowManager.damage;
        speed = arrowManager.speed;
        faceDir = arrowManager.faceDir;
        hitEffectGo = arrowManager.hitEffectGo;

        remainingPierce = arrowManager.pierceCount;
        explodeRadius = arrowManager.explodeRadius;
        explodeDamage = arrowManager.explodeDamage;
        explodeTargetMask = arrowManager.explodeTargetMask;

        canPierce = arrowManager.HasPierce();
        canExplode = arrowManager.HasExplode();
        canSingle = arrowManager.HasSingle();

        hasExploded = false;
        hitTargets.Clear();
        lastAttack = -999f;

        VFX_AutomationEffect vfx = GetComponent<VFX_AutomationEffect>();
        vfx?.SetupEffectGo(hitEffectGo, .5f);
    }

    protected override void Attack(Collider2D target)
    {
        if (canSingle)
        {
            base.Attack(target);
            return;
        }

        if (target == null) return;
        if (!CanAttack()) return;
        if (hitTargets.Contains(target)) return;

        lastAttack = Time.time;
        hitTargets.Add(target);

        IDamageable damageable = target.GetComponent<IDamageable>();
        if (damageable == null) return;

        bool targetHit = damageable.TakeDamage(damage);
        if (!targetHit) return;

        if (vfx != null)
            vfx.CreateEffect(target.transform);

        if (canExplode && !hasExploded)
            Explode(target.transform.position);

        if (canPierce && remainingPierce > 0)
        {
            remainingPierce--;
            return;
        }

        ObjectPool.Instance.Despawn(gameObject);
    }

    private void Explode(Vector3 center)
    {
        hasExploded = true;

        Collider2D[] hits = Physics2D.OverlapCircleAll(center, explodeRadius, explodeTargetMask);

        for (int i = 0; i < hits.Length; i++)
        {
            Collider2D hit = hits[i];
            if (hit == null) continue;

            IDamageable damageable = hit.GetComponent<IDamageable>();
            if (damageable == null) continue;

            if (hitTargets.Contains(hit))
                continue;

            hitTargets.Add(hit);
            damageable.TakeDamage(explodeDamage);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Attack(collision);
    }

    private void OnDisable()
    {
        hitTargets.Clear();
        hasExploded = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explodeRadius);
    }
}