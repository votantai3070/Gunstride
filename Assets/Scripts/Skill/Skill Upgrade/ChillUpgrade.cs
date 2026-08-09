using UnityEngine;

public class ChillUpgrade : MonoBehaviour, IProjectileUpgrade
{
    [SerializeField] private int slowStacksPerHit = 1;

    private ProjectileObject_Base projectile;

    public SkillUpgradeType upgradeType => SkillUpgradeType.Chill;

    public bool ShouldDespawn => true;

    public void Initialize(ProjectileObject_Base projectile, SkillBuffDataSO buff)
    {
        this.projectile = projectile;
    }

    public void OnHit(Collider2D target)
    {
        StatusEffectController status = target.GetComponent<StatusEffectController>();

        if (status == null)
            return;

        status.ApplySlow(slowStacksPerHit);
    }
}