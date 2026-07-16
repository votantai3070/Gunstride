public class Enemy_IdleState : EnemyState
{
    public Enemy_IdleState(Enemy enemy, StateMachine<EntityState> stateMachine, Projectile_Base projectile, string animBoolName) : base(enemy, stateMachine, projectile, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0) return;

        if (enemy.DetectedTarget() && projectile.CanUseSkill())
            stateMachine.ChangeState(enemy.attackState);
    }
}
