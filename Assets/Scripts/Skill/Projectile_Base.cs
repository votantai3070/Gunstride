using UnityEngine;

public class Projectile_Base : MonoBehaviour
{
    protected EntitySkillManager skillManager;

    [Header("Projectile Setup")]
    public SkillUpgradeType upgradeType = SkillUpgradeType.None;
    protected GameObject projectileObject;

    [Space]
    [SerializeField] protected Transform attackPoint;
    public int damage;
    public float speed;
    public float cooldown;
    [SerializeField] protected int projectileCount = 1;
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

        SetupUpgradeType(skillData);
    }

    private bool HasUpgrade(SkillUpgradeType type)
    {
        return (upgradeType & type) == type;
    }

    public virtual void CombineUpgrade(SkillBuffDataSO skillData)
    {
        ApplyUpgradeData(skillData);
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

    public virtual void AdditionalProjectileCount(int count)
    {
        projectileCount += count;
    }

    public virtual void SetupUpgradeType(SkillDataSO skillData)
    {
        if (upgradeType == SkillUpgradeType.None)
            upgradeType = skillData.upgradeType;
    }

    public virtual void ApplyUpgradeData(SkillBuffDataSO skillBuffData)
    {
        if (upgradeType == SkillUpgradeType.None)
            upgradeType = skillBuffData.upgradeData.upgradeType;
        else
            upgradeType |= skillBuffData.upgradeData.upgradeType;
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