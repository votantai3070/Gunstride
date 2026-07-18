using UnityEngine;

public class Projectile_Base : MonoBehaviour
{
    protected EntitySkillManager skillManager;

    [Header("Projectile Setup")]
    public SkillUpgradeType upgradeType = SkillUpgradeType.None;
    protected SkillDataSO.UpgradeData upgradeData;
    protected GameObject projectileObject;

    [Space]
    [SerializeField] protected Transform attackPoint;

    public int damage;
    public float speed;
    public float cooldown;
    public float faceDir { get; private set; }

    private float lastTimeAttack;

    public virtual void SetupProjectile(SkillDataSO skillData)
    {
        skillManager = GetComponentInParent<EntitySkillManager>();

        if (attackPoint == null)
            attackPoint = skillManager.entity.transform;

        faceDir = skillManager.entity.IsFlipped() ? -1 : 1;

        projectileObject = skillData.projectileObj;
        damage = skillData.damage;
        speed = skillData.speed + skillManager.entity.speed;

        ApplyUpgradeData(skillData);
    }

    private bool HasUpgrade(SkillUpgradeType type)
    {
        return (upgradeType & type) == type;
    }

    public virtual void CombineUpgrade(SkillDataSO skillData)
    {
        projectileObject = skillData.projectileObj;
        damage = Mathf.Max(damage, skillData.damage);
        speed = Mathf.Max(speed, skillData.speed + skillManager.entity.speed);

        ApplyUpgradeData(skillData);
    }

    public virtual void ApplyUpgradeData(SkillDataSO skillData)
    {
        upgradeData = skillData.upgradeData;

        if (upgradeType == SkillUpgradeType.None)
            upgradeType = skillData.upgradeData.upgradeType;
        else
            upgradeType |= skillData.upgradeData.upgradeType;

        cooldown = skillData.upgradeData.cooldown;
    }

    public virtual void UseSkill() { }

    public virtual bool CanUseSkill()
    {
        if (OnProjectileCooldown())
            return false;

        if (upgradeType == SkillUpgradeType.None)
            return false;

        if (!skillManager.entity.DetectedTarget())
            return false;

        return true;
    }

    #region Bool Upgrades
    protected bool HasSingle() => HasUpgrade(SkillUpgradeType.Single);
    protected bool HasTripleLane() => HasUpgrade(SkillUpgradeType.TripleLane);
    protected bool HasMultiSpawn() => HasUpgrade(SkillUpgradeType.MultiSpawn);
    protected bool HasPierce() => HasUpgrade(SkillUpgradeType.Pierce);
    protected bool HasExplode() => HasUpgrade(SkillUpgradeType.Explode);
    #endregion

    #region Cooldown
    public bool OnProjectileCooldown() => Time.time <= lastTimeAttack + cooldown;
    public void SetSkillOnCooldown() => lastTimeAttack = Time.time;
    public void ReduceCooldownBy(float cooldownReduction) => lastTimeAttack += cooldownReduction;
    public void ResetCooldown() => lastTimeAttack = Time.time - cooldown;
    #endregion
}