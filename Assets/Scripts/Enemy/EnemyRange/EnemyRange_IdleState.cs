public class EnemyRange_IdleState : EnemyState
{
    public EnemyRange_IdleState(Enemy enemy, StateMachine<EntityState> stateMachine, Projectile_Base projectile, string animBoolName) : base(enemy, stateMachine, projectile, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemyRange.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0) return;

        if (enemyRange.CanAttackTarget() && projectile.CanUseSkill())
            stateMachine.ChangeState(enemyRange.attackState);
    }
}
