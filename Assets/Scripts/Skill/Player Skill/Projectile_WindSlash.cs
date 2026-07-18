using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class Projectile_WindSlash : Projectile_Base
{
    private Player player;

    [Header("Triple Lane")]
    [SerializeField] private float[] laneOffsetsY = new float[] { -3f, 0f, 3f };

    [Header("Multi Spawn")]
    [SerializeField] private int amount = 1;
    [SerializeField] private float delayBetweenShots = 0.2f;

    private Coroutine fireMultipleRoutine;

    public override void SetupProjectile(SkillDataSO skillData)
    {
        player = GetComponentInParent<Player>();
        base.SetupProjectile(skillData);
    }

    public override void ApplyUpgradeData(SkillDataSO skillData)
    {
        base.ApplyUpgradeData(skillData);

        if ((skillData.upgradeData.upgradeType & SkillUpgradeType.MultiSpawn) == SkillUpgradeType.MultiSpawn)
        {
            amount = Mathf.Max(amount, skillData.upgradeData.amount);
            delayBetweenShots = skillData.upgradeData.delayBetweenShots;
        }
    }

    public override void CombineUpgrade(SkillDataSO skillData)
    {
        base.CombineUpgrade(skillData);

        if ((skillData.upgradeData.upgradeType & SkillUpgradeType.MultiSpawn) == SkillUpgradeType.MultiSpawn)
        {
            amount = Mathf.Max(amount, skillData.upgradeData.amount);
            delayBetweenShots = skillData.upgradeData.delayBetweenShots;
        }
    }

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
        if (HasTripleLane() && HasMultiSpawn())
        {
            FireTripleLaneMultiSpawn();
            return;
        }

        if (HasTripleLane())
        {
            FireTripleLane();
            return;
        }

        if (HasMultiSpawn())
        {
            FireMultiSpawn();
            return;
        }

        if (HasSingle() || upgradeType == SkillUpgradeType.None)
        {
            SpawnSlash(transform.position);
        }
    }

    private void FireTripleLane()
    {
        float currentLaneY = player.movement.GetCurrentLane();

        for (int i = 0; i < laneOffsetsY.Length; i++)
        {
            float laneY = laneOffsetsY[i];

            if (Mathf.Approximately(laneY, currentLaneY))
                continue;

            Vector3 spawnPos = new Vector3(transform.position.x, laneY, transform.position.z);
            SpawnSlash(spawnPos);
        }
    }

    private void FireMultiSpawn()
    {
        if (amount <= 0)
            return;

        if (fireMultipleRoutine != null)
            StopCoroutine(fireMultipleRoutine);

        fireMultipleRoutine = StartCoroutine(FireMultipleCo());
    }

    private IEnumerator FireMultipleCo()
    {
        for (int i = 0; i < amount; i++)
        {
            SpawnSlash(transform.position);

            if (i < amount - 1)
                yield return new WaitForSeconds(delayBetweenShots);
        }

        fireMultipleRoutine = null;
    }

    private void FireTripleLaneMultiSpawn()
    {
        if (amount <= 0)
            return;

        if (fireMultipleRoutine != null)
            StopCoroutine(fireMultipleRoutine);

        fireMultipleRoutine = StartCoroutine(FireTripleLaneMultiSpawnCo());
    }

    private IEnumerator FireTripleLaneMultiSpawnCo()
    {
        float currentLaneY = player.movement.GetCurrentLane();

        for (int i = 0; i < amount; i++)
        {
            for (int j = 0; j < laneOffsetsY.Length; j++)
            {
                float laneY = laneOffsetsY[j];

                if (Mathf.Approximately(laneY, currentLaneY))
                    continue;

                Vector3 spawnPos = new Vector3(transform.position.x, laneY, transform.position.z);
                SpawnSlash(spawnPos);
            }

            if (i < amount - 1)
                yield return new WaitForSeconds(delayBetweenShots);
        }

        fireMultipleRoutine = null;
    }

    private void SpawnSlash(Vector3 spawnPos)
    {
        ProjectileObject_WindSlash windSlash = ObjectPool.instance
            .Spawn(projectileObject.name, spawnPos, quaternion.identity, null)
            .GetComponent<ProjectileObject_WindSlash>();

        windSlash.SetupWindSlash(this);
    }

    private void OnDisable()
    {
        if (fireMultipleRoutine != null)
        {
            StopCoroutine(fireMultipleRoutine);
            fireMultipleRoutine = null;
        }
    }
}