public class Enemy_AttackState : EnemyState
{
    bool canUseSkill;

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

        if (enemy.isTrigger) return;

        if (enemy.isAttack)
        {
            projectile.UseSkill();
            enemy.isAttack = false;
        }

        if (enemy.isTrigger)
            if (enemy.DetectedTarget() == false || !canUseSkill)
                stateMachine.ChangeState(enemy.idleState);
    }
}
