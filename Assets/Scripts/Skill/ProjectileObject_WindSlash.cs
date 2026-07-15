using UnityEngine;

public class ProjectileObject_WindSlash : ProjectileObject_Base
{
    private Projectile_WindSlash windSlashManager;

    public void SetupWindSlash(Projectile_WindSlash windSlashManager)
    {
        this.windSlashManager = windSlashManager;
        damage = windSlashManager.damage;
        speed = windSlashManager.speed;
        faceLeftDir = windSlashManager.faceRightDir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        Attack(collision);
    }
}
