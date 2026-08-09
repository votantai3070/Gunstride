using UnityEngine;

public class PierceUpgrade : MonoBehaviour, IProjectileUpgrade
{
    [SerializeField] private int pierceCount = 0;
    private ProjectileObject_Base projectile;

    public SkillUpgradeType upgradeType => SkillUpgradeType.Pierce;
    public bool ShouldDespawn => pierceCount <= 0;

    public void Initialize(ProjectileObject_Base projectile, SkillBuffDataSO skillBuffData)
    {
        this.projectile = projectile;

        if (skillBuffData is ItemBuff_Pierce pierceData)
        {
            Debug.Log($"PierceUpgrade: Initialized with pierce count {pierceData.pierceCount}");
            pierceCount = projectile.pierceCount == 0 ? pierceData.pierceCount : projectile.bounceCount;
        }
    }

    public void OnHit(Collider2D target)
    {
        if (pierceCount > 0)
        {
            Debug.Log($"PierceUpgrade: Hit target {target.name}, remaining pierce count: {pierceCount - 1}");
            pierceCount--;
        }
    }
}