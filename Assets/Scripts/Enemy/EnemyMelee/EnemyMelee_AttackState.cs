public class EnemyMelee_AttackState : EnemyState
{
    public EnemyMelee_AttackState(Enemy enemy, StateMachine<EntityState> stateMachine, Projectile_Base projectile, string animBoolName) : base(enemy, stateMachine, projectile, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemyMelee.isTrigger = false;
        enemyMelee.isAttack = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemyMelee.isAttack)
        {
            enemyMelee.combat.TryAttack();
            enemyMelee.isAttack = false;
        }

        if (enemyMelee.isTrigger)
            stateMachine.ChangeState(enemyMelee.idleState);
    }
}
