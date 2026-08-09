using UnityEngine;

public class PierceUpgrade : MonoBehaviour, IProjectileUpgrade
{
    [SerializeField] private int pierceCount;

    private ProjectileObject_Base projectile;
    private bool shouldDespawn;

    public SkillUpgradeType upgradeType => SkillUpgradeType.Pierce;

    public bool ShouldDespawn => shouldDespawn;

    public void Initialize(
        ProjectileObject_Base projectile,
        SkillBuffDataSO skillBuffData)
    {
        this.projectile = projectile;
        shouldDespawn = false;

        if (skillBuffData is ItemBuff_Pierce pierceData)
        {
            pierceCount = Mathf.Max(0, pierceData.pierceCount);

            Debug.Log(
                $"PierceUpgrade initialized: {pierceCount} additional hit(s)"
            );
        }
    }

    public void OnHit(Collider2D target)
    {
        if (target == null)
            return;

        if (pierceCount <= 0)
        {
            shouldDespawn = true;

            Debug.Log($"PierceUpgrade: final hit on {target.name}");

            return;
        }

        pierceCount--;
        shouldDespawn = false;

        Debug.Log($"PierceUpgrade: hit {target.name}, " + $"remaining additional pierces: {pierceCount}");
    }

    private void OnDisable()
    {
        shouldDespawn = false;
        pierceCount = 0;
    }
}