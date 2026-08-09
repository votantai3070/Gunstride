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

        if (!enemyMelee.isFrozen)
            enemyMelee.SetVelocity(enemyMelee.speed);
        else
            enemyMelee.SetVelocity(0);

        if (enemyMelee.CanAttackTarget())
            stateMachine.ChangeState(enemyMelee.attackState);
    }
}
