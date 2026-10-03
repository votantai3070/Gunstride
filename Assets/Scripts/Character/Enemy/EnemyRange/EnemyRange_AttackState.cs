public class EnemyRange_AttackState : EnemyState
{
    public EnemyRange_AttackState(EnemyRange enemy, StateMachine<EntityState> stateMachine, Projectile_Base projectile, string animBoolName) : base(enemy, stateMachine, projectile, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemyRange.SetVelocity(0);
        enemyRange.isTrigger = false;
        enemyRange.isAttack = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemyRange.isAttack && enemyRange.combat.GetWeapon().CanShoot() && enemyRange.CanAttackTarget(enemyRange.combat.GetWeapon().range))
        {
            enemyRange.combat.Shoot();
        }

        if (enemyRange.isTrigger)
            stateMachine.ChangeState(enemyRange.idleState);
    }
}
