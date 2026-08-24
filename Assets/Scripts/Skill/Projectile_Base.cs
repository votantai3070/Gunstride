using System.Collections.Generic;
using UnityEngine;

public class Projectile_Base : MonoBehaviour
{
    public Entity Entity { get; private set; }
    public EntitySkillManager SkillManager { get; private set; }
    public ElementType ElementType { get; private set; }


    [Header("Projectile Setup")]
    public List<SkillBuffDataSO> SkillBuffData { get; private set; } = new();
    public SkillUpgradeType upgradeType = SkillUpgradeType.None;
    protected GameObject projectileObject;

    [Space]
    [SerializeField] protected Transform attackPoint;
    public int Damage { get; private set; }
    public float Speed { get; private set; }
    public float Cooldown { get; private set; }

    [Header("Upgrade Data")]
    [SerializeField] protected int projectileCount = 1;
    public int PierceCount { get; private set; }
    public int BounceCount { get; private set; }
    protected float explosionRadius;
    protected int explosionDamage;
    public LayerMask WhatIsTarget { get; private set; }
    [SerializeField] protected float delayBetweenShots = 0.2f;

    public float FaceDir { get; private set; }

    private float lastTimeAttack;
    protected Coroutine fireRoutine;

    public virtual void SetupProjectile(SkillDataSO skillData)
    {
        Entity = GetComponentInParent<Entity>();
        SkillManager = GetComponentInParent<EntitySkillManager>();

        if (attackPoint == null)
            attackPoint = SkillManager.entity.transform;

        FaceDir = SkillManager.entity.IsFlipped() ? -1 : 1;

        projectileObject = skillData.projectileObj;
        Damage = skillData.damage;
        Speed = skillData.speed + SkillManager.entity.speed;
        delayBetweenShots = skillData.delayBetweenShots;
        Cooldown = skillData.cooldown;

        upgradeType = skillData.upgradeType;
    }

    public void SetElementType(ElementType type)
    {
        ElementType = type;
    }

    public bool HasUpgrade(SkillUpgradeType type)
    {
        return (upgradeType & type) == type;
    }

    public virtual void UseSkill() { }

    public virtual bool CanUseSkill()
    {
        if (SkillManager is PlayerSkillManager playerManager)
        {
            if (playerManager.player.movement.isChangingLane)
                return false;
        }

        if (OnProjectileCooldown())
            return false;

        if (upgradeType == SkillUpgradeType.None)
            return false;

        if (!SkillManager.entity.CanAttackTarget())
            return false;

        return true;
    }

    public virtual void CombineUpgrade(SkillBuffDataSO skillData)
    {
        ApplyUpgradeData(skillData);
    }

    public virtual void RemoveUpgrade(SkillBuffDataSO skillData)
    {
        RemoveUpgradeData(skillData);
    }

    protected virtual void ApplyUpgradeData(SkillBuffDataSO skillBuffData)
    {
        if (skillBuffData == null) return;

        upgradeType = upgradeType == SkillUpgradeType.None
            ? skillBuffData.upgradeType
            : upgradeType | skillBuffData.upgradeType;

        if (!SkillBuffData.Contains(skillBuffData))
            SkillBuffData.Add(skillBuffData);

        ApplyBuff(skillBuffData);
    }

    protected virtual void RemoveUpgradeData(SkillBuffDataSO skillBuffData)
    {
        if (skillBuffData == null)
            return;

        SkillBuffData.Remove(skillBuffData);

        upgradeType &= ~skillBuffData.upgradeType;
        RemoveBuff(skillBuffData);
    }

    private void ApplyBuff(SkillBuffDataSO skillBuffData)
    {
        if (skillBuffData is ItemBuff_Additional add)
            AdditionalProjectile(add.amount);
        if (skillBuffData is ItemBuff_Bounce bounce)
            AdditionalBounceCount(bounce.bounceCount);
        if (skillBuffData is ItemBuff_Pierce pierce)
            AdditionalPierceCount(pierce.pierceCount);
        if (skillBuffData is ItemBuff_Explode explode)
            ApplyExplode(explode);
    }

    private void RemoveBuff(SkillBuffDataSO skillBuffData)
    {
        if (skillBuffData is ItemBuff_Additional add)
            RemoveProjectile(add.amount);
        if (skillBuffData is ItemBuff_Bounce bounce)
            RemoveBounceCount(bounce.bounceCount);
        if (skillBuffData is ItemBuff_Pierce pierce)
            RemovePierceCount(pierce.pierceCount);
        if (skillBuffData is ItemBuff_Explode explode)
            RemoveExplode(explode);
    }

    private SkillBuffDataSO GetBuffByType(SkillUpgradeType skillUpgrade)
    {
        return skillUpgrade switch
        {
            SkillUpgradeType.Add => ScriptableObject.CreateInstance<ItemBuff_Additional>(),
            SkillUpgradeType.Bounce => ScriptableObject.CreateInstance<ItemBuff_Bounce>(),
            SkillUpgradeType.Pierce => ScriptableObject.CreateInstance<ItemBuff_Pierce>(),
            SkillUpgradeType.Explode => ScriptableObject.CreateInstance<ItemBuff_Explode>(),
            SkillUpgradeType _ => null
        };
    }

    #region Skill Buff
    // Additional Projectile Skill
    private void AdditionalProjectile(int amount) => projectileCount = Mathf.Clamp(projectileCount + amount, 1, 3);
    private void RemoveProjectile(int amount) => projectileCount = Mathf.Clamp(projectileCount - amount, 1, 3);

    // Pierce Skill
    private void AdditionalPierceCount(int amount) => PierceCount = Mathf.Clamp(PierceCount + amount, 1, 3);
    private void RemovePierceCount(int amount) => PierceCount = Mathf.Clamp(PierceCount - amount, 1, 3);

    // Bounce Skill
    private void AdditionalBounceCount(int amount) => BounceCount = Mathf.Clamp(BounceCount + amount, 1, 3);
    private void RemoveBounceCount(int amount) => BounceCount = Mathf.Clamp(BounceCount - amount, 1, 3);

    // Explode Skill
    private void ApplyExplode(ItemBuff_Explode explode)
    {
        explosionDamage = explode.explosionDamage;
        explosionRadius = explode.explosionRadius;
        WhatIsTarget = explode.explodeTargetMask;
    }

    private void RemoveExplode(ItemBuff_Explode explode)
    {
        explosionDamage = 0;
        explosionRadius = 0;
        WhatIsTarget = ~explode.explodeTargetMask;
    }

    #endregion

    #region Cooldown
    public bool OnProjectileCooldown() => Time.time < lastTimeAttack + Cooldown;
    public void SetSkillOnCooldown() => lastTimeAttack = Time.time;
    public void ReduceCooldownBy(float cooldownReduction) => lastTimeAttack -= cooldownReduction;
    public void ResetCooldown() => lastTimeAttack = Time.time - Cooldown;
    #endregion
}