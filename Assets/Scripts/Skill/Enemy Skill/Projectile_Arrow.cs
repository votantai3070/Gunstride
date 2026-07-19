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
        ApplyArrowUpgradeData(skillData);
    }

    public override void CombineUpgrade(SkillDataSO skillData)
    {
        base.CombineUpgrade(skillData);
        ApplyArrowUpgradeData(skillData);
    }

    private void ApplyArrowUpgradeData(SkillDataSO skillData)
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
        if (HasSingle() || upgradeType == SkillUpgradeType.None)
        {
            CreateArrow(attackPoint.position);
        }

        SetSkillOnCooldown();
    }

    private void CreateArrow(Vector3 spawnPos)
    {
        ProjectileObject_Arrow arrowGo =
            ObjectPool.instance.Spawn(projectileObject.name, spawnPos, Quaternion.identity, null)
            .GetComponent<ProjectileObject_Arrow>();

        arrowGo.SetupArrow(this);
    }
}