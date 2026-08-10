using UnityEngine;

public class ChillUpgrade : MonoBehaviour, IProjectileUpgrade
{
    public SkillUpgradeType upgradeType => SkillUpgradeType.Chill;

    public bool ShouldDespawn => true;

    public void Initialize(ProjectileObject_Base projectile, SkillBuffDataSO buff)
    {
    }

    public void OnHit(Collider2D target)
    {

    }
}