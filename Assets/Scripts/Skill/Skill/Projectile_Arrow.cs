using System.Collections;
using UnityEngine;

public class Projectile_Arrow : Projectile_Base
{
    public override void SetupProjectile(SkillDataSO skillData)
    {
        base.SetupProjectile(skillData);
    }

    public override void CombineUpgrade(SkillBuffDataSO skillData)
    {
        base.CombineUpgrade(skillData);
    }

    protected override void ApplyUpgradeData(SkillBuffDataSO skillBuffData)
    {
        base.ApplyUpgradeData(skillBuffData);
    }

    public override void UseSkill()
    {
        FireSpawn();

        SetSkillOnCooldown();
    }

    private void CreateArrow(Vector3 spawnPos)
    {
        ProjectileObject_Arrow arrowGo =
            ObjectPool.Instance.Spawn(projectileObject.name, spawnPos, Quaternion.identity, null)
            .GetComponent<ProjectileObject_Arrow>();


        GameObject elementEffect = Entity.EntityEffects.GetElementVfx(ElementType);
        if (elementEffect)
        {
            ObjectPool.Instance.Spawn(elementEffect.name, arrowGo.transform.position, Quaternion.identity, arrowGo.transform);
        }

        arrowGo.SetupProjectile(this);

    }

    private void FireSpawn()
    {
        if (projectileCount <= 0)
            return;

        if (fireRoutine != null)
            StopCoroutine(fireRoutine);

        fireRoutine = StartCoroutine(FireCo());
    }

    private IEnumerator FireCo()
    {
        for (int i = 0; i < projectileCount; i++)
        {
            CreateArrow(attackPoint.position);

            if (i < projectileCount - 1)
                yield return new WaitForSeconds(delayBetweenShots);
        }

        fireRoutine = null;
    }
}