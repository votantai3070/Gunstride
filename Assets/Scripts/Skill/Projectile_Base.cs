using UnityEngine;

public class Projectile_Base : MonoBehaviour
{
    protected EntitySkillManager skillManager;

    [Header("Projectile Setup")]
    public SkillType skillType;
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

        skillType = skillData.skillType;
        projectileObject = skillData.projectileObj;
        damage = skillData.damage;
        speed = skillData.speed;
        cooldown = skillData.cooldown;
    }

    public virtual void UseSkill() { }

    public virtual bool CanUseSkill()
    {
        if (OnProjectileCooldown())
            return false;

        if (skillType == SkillType.None)
            return false;

        if (!skillManager.entity.DetectedTarget())
            return false;

        return true;
    }

    public bool OnProjectileCooldown() => Time.time <= lastTimeAttack + cooldown;
    public void SetSkillOnCooldown() => lastTimeAttack = Time.time;
    public void ReduceCooldownBy(float cooldownReduction) => lastTimeAttack += cooldownReduction;
    public void ResetCooldown() => lastTimeAttack = Time.time - cooldown;
}
