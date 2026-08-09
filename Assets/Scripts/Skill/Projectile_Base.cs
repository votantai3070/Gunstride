using System.Collections.Generic;
using UnityEngine;

public class Projectile_Base : MonoBehaviour
{
    protected EntitySkillManager skillManager;

    [Header("Projectile Setup")]
    public List<SkillBuffDataSO> skillBuffData { get; private set; } = new();
    public SkillUpgradeType upgradeType = SkillUpgradeType.None;
    protected GameObject projectileObject;

    [Space]
    [SerializeField] protected Transform attackPoint;
    public int damage { get; private set; }
    public float speed { get; private set; }
    public float cooldown { get; private set; }
    [SerializeField] protected int projectileCount = 1;
    public int pierceCount { get; set; }
    public int bounceCount { get; private set; }
    [SerializeField] protected float delayBetweenShots = 0.2f;

    public float faceDir { get; private set; }

    private float lastTimeAttack;
    protected Coroutine fireRoutine;

    public virtual void SetupProjectile(SkillDataSO skillData)
    {
        skillManager = GetComponentInParent<EntitySkillManager>();

        if (attackPoint == null)
            attackPoint = skillManager.entity.transform;

        faceDir = skillManager.entity.IsFlipped() ? -1 : 1;

        projectileObject = skillData.projectileObj;
        damage = skillData.damage;
        speed = skillData.speed + skillManager.entity.speed;
        delayBetweenShots = skillData.delayBetweenShots;
        cooldown = skillData.cooldown;

        upgradeType = skillData.upgradeType;
    }

    public bool HasUpgrade(SkillUpgradeType type)
    {
        return (upgradeType & type) == type;
    }


    public virtual void UseSkill() { }

    public virtual bool CanUseSkill()
    {
        if (skillManager is PlayerSkillManager playerManager)
        {
            if (playerManager.player.movement.isChangingLane)
                return false;
        }

        if (OnProjectileCooldown())
            return false;

        if (upgradeType == SkillUpgradeType.None)
            return false;

        if (!skillManager.entity.CanAttackTarget())
            return false;

        return true;
    }

    public virtual void CombineUpgrade(SkillBuffDataSO skillData)
    {
        ApplyUpgradeData(skillData);
    }

    public virtual void ApplyUpgradeData(SkillBuffDataSO skillBuffData)
    {
        if (skillBuffData == null) return;

        upgradeType = upgradeType == SkillUpgradeType.None
            ? skillBuffData.upgradeType
            : upgradeType | skillBuffData.upgradeType;

        if (skillBuffData != null && !this.skillBuffData.Contains(skillBuffData))
            this.skillBuffData.Add(skillBuffData);
    }

    public virtual void AdditionalProjectile(int amount)
    {
        projectileCount += amount;
    }

    public virtual void AdditionalPierceCount(int amount)
    {
        pierceCount += amount;
    }

    public virtual void AdditionalBounceCount(int amount)
    {
        bounceCount += amount;
    }

    #region Bool Upgrades
    public bool HasSingle() => HasUpgrade(SkillUpgradeType.Single);
    protected bool HasTripleLane() => HasUpgrade(SkillUpgradeType.TripleLane);
    protected bool HasMultiSpawn() => HasUpgrade(SkillUpgradeType.MultiSpawn);
    public bool HasPierce() => HasUpgrade(SkillUpgradeType.Pierce);
    public bool HasExplode() => HasUpgrade(SkillUpgradeType.Explode);
    #endregion

    #region Cooldown
    public bool OnProjectileCooldown() => Time.time <= lastTimeAttack + cooldown;
    public void SetSkillOnCooldown() => lastTimeAttack = Time.time;
    public void ReduceCooldownBy(float cooldownReduction) => lastTimeAttack += cooldownReduction;
    public void ResetCooldown() => lastTimeAttack = Time.time - cooldown;
    #endregion
}