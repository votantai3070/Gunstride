using UnityEngine;

public class Projectile_Arrow : Projectile_Base
{
    private Enemy enemy;

    public override void SetupProjectile(SkillDataSO skillData)
    {
        enemy = GetComponentInParent<Enemy>();

        base.SetupProjectile(skillData);
    }

    public override void UseSkill()
    {
        Debug.Log("Arrow shot");
        CreateArrow();
        SetSkillOnCooldown();
    }

    private void CreateArrow()
    {
        GameObject arrowGo = ObjectPool.instance.Spawn(projectileObject.name, attackPoint.position, Quaternion.identity, null);
        arrowGo.GetComponent<ProjectileObject_Arrow>().SetupArrow(this);
    }
}
