public class EnemyState : EntityState
{
    protected EnemyMelee enemyMelee;
    protected EnemyRange enemyRange;

    public EnemyState(Enemy enemy, StateMachine<EntityState> stateMachine, Projectile_Base projectile, string animBoolName) : base(stateMachine, projectile, animBoolName)
    {
        enemyMelee = enemy as EnemyMelee;
        enemyRange = enemy as EnemyRange;
        anim = enemy.anim;
        col = enemy.col;
    }
}
