using UnityEngine;

public class Projectile_Base : MonoBehaviour
{
    private PlayerSkillManager skillManager;
    protected GameObject projectileObject;

    [Header("Projectile Setup")]
    public SkillType skillType;
    public int damage;
    public float speed;
    [SerializeField] protected float cooldown;

    public float faceDir { get; private set; }
    private float lastTimeAttack;

    public virtual void SetupProjectile(SkillDataSO skillData)
    {
        skillManager = GetComponentInParent<PlayerSkillManager>();
        faceDir = skillManager.player.IsFlipped() ? -1 : 1;

        skillType = skillData.skillType;
        projectileObject = skillData.projectileObj;
        damage = skillData.damage;
        speed = skillData.speed;
        cooldown = skillData.cooldown;
    }

    public virtual void UseSkill() { }

    public bool CanUseSkill()
    {
        if (OnProjectileCooldown())
            return false;

        if (skillType == SkillType.None)
            return false;

        if (!skillManager.player.DetectedTarget())
            return false;

        if (skillManager.player.movement.isChangingLane)
            return false;

        return true;
    }

    public bool OnProjectileCooldown() => Time.time < lastTimeAttack + cooldown;
    public void SetSkillOnCooldown() => lastTimeAttack = Time.time;
    public void ReduceCooldownBy(float cooldownReduction) => lastTimeAttack = lastTimeAttack + cooldownReduction;
    public void ResetCooldown() => lastTimeAttack = Time.time - cooldown;
}
