using Unity.Mathematics;

public class Projectile_WindSlash : Projectile_Base
{
    public override void UseSkill()
    {
        CreateWindSlash();
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

    private void CreateWindSlash()
    {
        ProjectileObject_WindSlash windSlash =
             ObjectPool.instance.Spawn(projectileObject.name, transform.position, quaternion.identity, null).GetComponent<ProjectileObject_WindSlash>();

        windSlash.SetupWindSlash(this);
    }
}
