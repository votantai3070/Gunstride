using System.Collections.Generic;
using UnityEngine;

public class BounceUpgrade : MonoBehaviour, IProjectileUpgrade
{
    [SerializeField] private int bounceCount;
    [SerializeField] private float bounceRadius;
    [SerializeField] private LayerMask targetMask;

    private ProjectileObject_Base projectile;
    private readonly HashSet<Collider2D> bouncedTargets = new();

    private bool shouldDespawn;

    public SkillUpgradeType upgradeType => SkillUpgradeType.Bounce;
    public bool ShouldDespawn => shouldDespawn;

    public void Initialize(ProjectileObject_Base projectile, SkillBuffDataSO buff)
    {
        this.projectile = projectile;

        bouncedTargets.Clear();
        shouldDespawn = false;

        if (buff is ItemBuff_Bounce bounceData)
        {
            bounceCount = projectile.bounceCount == 0 ? bounceData.bounceCount : projectile.bounceCount;
            bounceRadius = Mathf.Max(0f, bounceData.bounceRadius);
            targetMask = bounceData.targetMask;
        }
    }

    public void OnHit(Collider2D target)
    {
        shouldDespawn = true;

        if (bounceCount <= 0)
            return;

        Collider2D nextTarget = FindClosestTarget(target);

        if (nextTarget == null)
            return;

        bounceCount--;

        bouncedTargets.Add(target);
        bouncedTargets.Add(nextTarget);

        Vector2 direction =
            nextTarget.transform.position - projectile.transform.position;

        projectile.SetDirection(direction);

        shouldDespawn = false;
    }

    private Collider2D FindClosestTarget(Collider2D currentTarget)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            projectile.transform.position,
            bounceRadius,
            targetMask
        );

        Collider2D closestTarget = null;
        float closestDistance = float.MaxValue;

        foreach (Collider2D hit in hits)
        {
            if (hit == null)
                continue;

            if (hit == currentTarget)
                continue;

            if (bouncedTargets.Contains(hit))
                continue;

            if (projectile.HasHitTarget(hit))
                continue;

            float distance = Vector2.Distance(
                projectile.transform.position,
                hit.transform.position
            );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = hit;
            }
        }

        return closestTarget;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        Vector3 center = transform.position;
        center.z = 0f;

        Gizmos.DrawWireSphere(center, bounceRadius);
    }

    private void OnDisable()
    {
        bouncedTargets.Clear();
        shouldDespawn = false;
    }
}