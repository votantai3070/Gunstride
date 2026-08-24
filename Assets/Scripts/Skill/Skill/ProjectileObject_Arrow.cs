using UnityEngine;

public class ProjectileObject_Arrow : ProjectileObject_Base
{
    private Projectile_Arrow arrowManager;

    [Header("Arrow Runtime")]
    [SerializeField] private int remainingPierce;


    public void SetupProjectile(Projectile_Arrow arrowManager)
    {

        this.arrowManager = arrowManager;
        projectileManager = arrowManager;

        damage = arrowManager.Damage;
        speed = arrowManager.Speed;
        faceDir = arrowManager.FaceDir;
        moveDirection = new Vector2(faceDir, 0);

        bounceCount = arrowManager.BounceCount;
        pierceCount = arrowManager.PierceCount;

        upgradeType = arrowManager.upgradeType;
        activeBuffs = arrowManager.SkillBuffData;
        whatIsTarget = arrowManager.WhatIsTarget;

        elementType = arrowManager.ElementType;
        elementEffectData = arrowManager.Entity.EntityCombat.GetElementalEffectData();

        base.SetupProjectile();

        hitTargets.Clear();
        lastAttack = -999f;
    }
}