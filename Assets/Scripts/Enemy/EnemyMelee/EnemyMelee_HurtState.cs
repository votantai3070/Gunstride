public class EnemyMelee_HurtState : EnemyState
{
    public EnemyMelee_HurtState(Enemy enemy, StateMachine<EntityState> stateMachine, Projectile_Base projectile, string animBoolName) : base(enemy, stateMachine, projectile, animBoolName)
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

        bool frozen =
           enemyMelee.stateHandler != null && enemyMelee.stateHandler.IsFrozen();

        bool thunder =
              enemyMelee.stateHandler != null && enemyMelee.stateHandler.IsThunder();

        if (!frozen || !thunder)
        {
            stateMachine.ChangeState(enemyMelee.idleState);
        }
    }
}
