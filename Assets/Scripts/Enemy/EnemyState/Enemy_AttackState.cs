public class Enemy_AttackState : EnemyState
{
    public Enemy_AttackState(Enemy enemy, StateMachine<EntityState> stateMachine, Projectile_Base projectile, string animBoolName) : base(enemy, stateMachine, projectile, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemy.isTrigger = false;
        enemy.isAttack = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.isAttack && projectile.CanUseSkill())
        {
            projectile.UseSkill();
            enemy.isAttack = false;
        }

        if (enemy.isTrigger)
            stateMachine.ChangeState(enemy.idleState);
    }
}
