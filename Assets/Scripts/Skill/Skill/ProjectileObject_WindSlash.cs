public class ProjectileObject_WindSlash : ProjectileObject_Base
{
    private Projectile_WindSlash windSlashManager;

    public void SetupWindSlash(Projectile_WindSlash windSlashManager)
    {
        this.windSlashManager = windSlashManager;
        damage = windSlashManager.Damage;
        speed = windSlashManager.Speed;
        faceDir = windSlashManager.FaceDir;
    }
}
