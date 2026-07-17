using Unity.Mathematics;
using UnityEngine;

public enum WindSlashPattern
{
    Single,
    TripleLane,
    MultiSpawn
}

public class Projectile_WindSlash : Projectile_Base
{
    [SerializeField] private WindSlashPattern pattern;

    [Header("Triple Lane")]
    [SerializeField] private float[] laneOffsetsY = new float[] { -3f, 0f, 3f };

    [Header("Multi Spawn")]
    [SerializeField] private int amount = 3;
    [SerializeField] private float multiSpacingY = 1.25f;
    [SerializeField] private bool centerMultiSpawn = true;

    public override void UseSkill()
    {
        FireByPattern();
        SetSkillOnCooldown();
    }

    public override bool CanUseSkill()
    {
        if (skillManager is PlayerSkillManager playerManager)
        {
            if (playerManager.player.movement.isChangingLane)
                return false;
        }

        return base.CanUseSkill();
    }

    private void FireByPattern()
    {
        if (pattern == WindSlashPattern.TripleLane)
            FireTripleLane();

        if (pattern == WindSlashPattern.MultiSpawn)
            FireMultiSpawn();

        if (pattern == WindSlashPattern.Single)
            SpawnSlash(transform.position);
    }

    private void FireTripleLane()
    {
        for (int i = 0; i < laneOffsetsY.Length; i++)
        {
            Vector3 spawnPos = transform.position + new Vector3(0f, laneOffsetsY[i], 0f);
            SpawnSlash(spawnPos);
        }
    }

    private void FireMultiSpawn()
    {
        if (amount <= 0)
            return;

        if (amount == 1)
        {
            SpawnSlash(transform.position);
            return;
        }

        float startOffset = 0f;

        if (centerMultiSpawn)
            startOffset = -((amount - 1) * multiSpacingY) * 0.5f;

        for (int i = 0; i < amount; i++)
        {
            float yOffset = centerMultiSpawn
                ? startOffset + i * multiSpacingY
                : i * multiSpacingY;

            Vector3 spawnPos = transform.position + new Vector3(0f, yOffset, 0f);
            SpawnSlash(spawnPos);
        }
    }

    private void SpawnSlash(Vector3 spawnPos)
    {
        ProjectileObject_WindSlash windSlash = ObjectPool.instance
            .Spawn(projectileObject.name, spawnPos, quaternion.identity, null)
            .GetComponent<ProjectileObject_WindSlash>();

        windSlash.SetupWindSlash(this);
    }
}