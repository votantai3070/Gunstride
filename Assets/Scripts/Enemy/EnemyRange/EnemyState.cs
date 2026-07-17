public class EnemyState : EntityState
{
    protected EnemyRange enemyRange;

    public EnemyState(Enemy enemy, StateMachine<EntityState> stateMachine, Projectile_Base projectile, string animBoolName) : base(stateMachine, projectile, animBoolName)
    {
        enemyRange = enemy as EnemyRange;
        anim = enemy.anim;
        col = enemy.col;
    }

    public override void Enter()
    {
        base.Enter();

        enemyRange.FlippedLeft();
    }
}
