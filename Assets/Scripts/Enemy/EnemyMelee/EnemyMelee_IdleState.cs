public class EnemyMelee_IdleState : EnemyState
{
    public EnemyMelee_IdleState(Enemy enemy, StateMachine<EntityState> stateMachine, Projectile_Base projectile, string animBoolName) : base(enemy, stateMachine, projectile, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemyMelee.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0) return;

        if (enemyMelee.DetectedTarget())
            stateMachine.ChangeState(enemyMelee.attackState);
    }
}
