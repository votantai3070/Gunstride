public class EnemyMelee_BlockState : EnemyState
{
    public EnemyMelee_BlockState(Enemy enemy, StateMachine<EntityState> stateMachine, Projectile_Base projectile, string animBoolName) : base(enemy, stateMachine, projectile, animBoolName)
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

        if (enemyMelee.CanAttackTarget())
            stateMachine.ChangeState(enemyMelee.attackState);
    }
}
