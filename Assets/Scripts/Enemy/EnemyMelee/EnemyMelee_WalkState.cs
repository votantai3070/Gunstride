public class EnemyMelee_WalkState : EnemyState
{
    public EnemyMelee_WalkState(Enemy enemy, StateMachine<EntityState> stateMachine, Projectile_Base projectile, string animBoolName) : base(enemy, stateMachine, projectile, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        enemyMelee.SetVelocity(enemyMelee.speed);

        if (enemyMelee.DetectedTarget())
            stateMachine.ChangeState(enemyMelee.attackState);
    }
}
