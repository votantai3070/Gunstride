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

        damage = arrowManager.damage;
        speed = arrowManager.speed;
        faceDir = arrowManager.faceDir;
        moveDirection = new Vector2(faceDir, 0);

        bounceCount = arrowManager.bounceCount;
        pierceCount = arrowManager.pierceCount;
        Debug.Log("Pierce?? : " + pierceCount);

        upgradeType = arrowManager.upgradeType;
        activeBuffs = arrowManager.skillBuffData;
        whatIsTarget = arrowManager.whatIsTarget;

        base.SetupProjectile();

        hitTargets.Clear();
        lastAttack = -999f;
    }
}