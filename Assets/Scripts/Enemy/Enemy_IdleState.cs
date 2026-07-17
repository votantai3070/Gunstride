public class Enemy_IdleState : EnemyState
{
    public Enemy_IdleState(Enemy enemy, StateMachine<EntityState> stateMachine, Projectile_Base projectile, string animBoolName) : base(enemy, stateMachine, projectile, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemyRange != null
            ? enemyRange.idleTime : enemyMelee != null
            ? enemyMelee.idleTime : 0;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0) return;

        if (enemyMelee && enemyMelee.DetectedTarget())
            stateMachine.ChangeState(enemyMelee.attackState);

        if (enemyRange && enemyRange.DetectedTarget() && projectile.CanUseSkill())
            stateMachine.ChangeState(enemyRange.attackState);
    }
}
