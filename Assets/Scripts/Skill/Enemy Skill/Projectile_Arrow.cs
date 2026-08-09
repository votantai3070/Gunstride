using System.Collections;
using UnityEngine;

public class Projectile_Arrow : Projectile_Base
{
    [Header("Arrow Upgrade Data")]
    public float explosionRadius { get; private set; }
    public int explosionDamage { get; private set; }
    public LayerMask whatIsTarget { get; private set; }


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
        if (skillData is ItemBuff_Pierce pierce &&
            (skillData.upgradeType & SkillUpgradeType.Pierce) == SkillUpgradeType.Pierce)
        {
            pierceCount = Mathf.Max(pierceCount, pierce.pierceCount);
        }

        if (skillData is ItemBuff_Explode explode &&
            (skillData.upgradeType & SkillUpgradeType.Explode) == SkillUpgradeType.Explode)
        {
            explosionRadius = Mathf.Max(explosionRadius, explode.explosionRadius);
            explosionDamage = Mathf.Max(explosionDamage, explode.explosionDamage);
            whatIsTarget = explode.explodeTargetMask;
        }
    }

    public override void ApplyUpgradeData(SkillBuffDataSO skillBuffData)
    {
        base.ApplyUpgradeData(skillBuffData);

        ApplyArrowUpgradeData(skillBuffData);
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

        arrowGo.SetupProjectile(this);
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