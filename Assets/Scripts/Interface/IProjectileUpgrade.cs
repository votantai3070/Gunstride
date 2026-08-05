using UnityEngine;

public interface IProjectileUpgrade
{
    SkillUpgradeType upgradeType { get; }
    void Initialize(ProjectileObject_Base projectile, SkillBuffDataSO buff);
    void OnHit(Collider2D target);
    bool ShouldDespawn { get; }
}