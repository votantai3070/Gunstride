using System.Collections;
using UnityEngine;

public class Projectile_Arrow : Projectile_Base
{
    [Header("Arrow Upgrade Data")]
    public int pierceCount { get; private set; }
    public float explodeRadius { get; private set; }
    public int explodeDamage { get; private set; }
    public LayerMask explodeTargetMask { get; private set; }


    public override void SetupProjectile(SkillDataSO skillData)
    {
        base.SetupProjectile(skillData);
    }

    public override void CombineUpgrade(SkillBuffDataSO skillData)
    {
        base.CombineUpgrade(skillData);
        ApplyArrowUpgradeData(skillData);
    }

    private void ApplyArrowUpgradeData(SkillBuffDataSO skillData)
    {
        if (HasPierce() ||
            (skillData.upgradeData.upgradeType & SkillUpgradeType.Pierce) == SkillUpgradeType.Pierce)
        {
            pierceCount = Mathf.Max(pierceCount, skillData.upgradeData.pierceCount);
        }

        if (HasExplode() ||
            (skillData.upgradeData.upgradeType & SkillUpgradeType.Explode) == SkillUpgradeType.Explode)
        {
            explodeRadius = Mathf.Max(explodeRadius, skillData.upgradeData.explodeRadius);
            explodeDamage = Mathf.Max(explodeDamage, skillData.upgradeData.explodeDamage);
            explodeTargetMask = skillData.upgradeData.explodeTargetMask;
        }
    }

    public override void UseSkill()
    {
        FireSpawn();

        SetSkillOnCooldown();
    }

    private void CreateArrow(Vector3 spawnPos)
    {
        ProjectileObject_Arrow arrowGo =
            ObjectPool.Instance.Spawn(projectileObject.name, spawnPos, Quaternion.identity, null)
            .GetComponent<ProjectileObject_Arrow>();

        arrowGo.SetupArrow(this);
    }

    private void FireSpawn()
    {
        if (projectileCount <= 0)
            return;

        if (fireRoutine != null)
            StopCoroutine(fireRoutine);

        fireRoutine = StartCoroutine(FireCo());
    }

    private IEnumerator FireCo()
    {
        for (int i = 0; i < projectileCount; i++)
        {
            CreateArrow(attackPoint.position);

            if (i < projectileCount - 1)
                yield return new WaitForSeconds(delayBetweenShots);
        }

        fireRoutine = null;
    }
}