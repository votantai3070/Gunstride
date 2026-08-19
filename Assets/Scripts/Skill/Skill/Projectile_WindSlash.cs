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

    public override void SetupProjectile(SkillDataSO skillData)
    {
        player = GetComponentInParent<Player>();
        base.SetupProjectile(skillData);
    }

    protected override void ApplyUpgradeData(SkillBuffDataSO skillData)
    {
        base.ApplyUpgradeData(skillData);

        //if ((skillData.upgradeData.upgradeType & SkillUpgradeType.MultiSpawn) == SkillUpgradeType.MultiSpawn)
        //{
        //    amount = Mathf.Max(amount, skillData.upgradeData.amount);
        //}
    }

    public override void CombineUpgrade(SkillBuffDataSO skillData)
    {
        base.CombineUpgrade(skillData);
    }

    public override void UseSkill()
    {
        FireSpawn();
        SetSkillOnCooldown();
    }


    private void FireSpawn()
    {
        if (amount <= 0)
            return;

        if (fireRoutine != null)
            StopCoroutine(fireRoutine);

        fireRoutine = StartCoroutine(FireCo());
    }

    private IEnumerator FireCo()
    {
        for (int i = 0; i < amount; i++)
        {
            CreateSlash(transform.position);

            if (i < amount - 1)
                yield return new WaitForSeconds(delayBetweenShots);
        }

        fireRoutine = null;
    }

    private void CreateSlash(Vector3 spawnPos)
    {
        ProjectileObject_WindSlash windSlash = ObjectPool.Instance
            .Spawn(projectileObject.name, spawnPos, quaternion.identity, null)
            .GetComponent<ProjectileObject_WindSlash>();

        windSlash.SetupWindSlash(this);
    }

    private void OnDisable()
    {
        if (fireRoutine != null)
        {
            StopCoroutine(fireRoutine);
            fireRoutine = null;
        }
    }
}