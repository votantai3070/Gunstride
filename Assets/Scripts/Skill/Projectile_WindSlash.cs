using Unity.Mathematics;

public class Projectile_WindSlash : Projectile_Base
{
    public override void UseSkill()
    {
        CreateWindSlash();
        SetSkillOnCooldown();
    }

    private void CreateWindSlash()
    {
        ProjectileObject_WindSlash windSlash =
             ObjectPool.instance.Spawn(projectileObject.name, transform.position, quaternion.identity).GetComponent<ProjectileObject_WindSlash>();

        windSlash.SetupWindSlash(this);
    }
}
