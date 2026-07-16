public class ProjectileObject_Arrow : ProjectileObject_Base
{
    private Projectile_Arrow arrowManager;

    public void SetupArrow(Projectile_Arrow arrowManager)
    {
        this.arrowManager = arrowManager;
        damage = arrowManager.damage;
        speed = arrowManager.speed;
        faceDir = arrowManager.faceDir;
    }
}
