public class EnemyState : EntityState
{
    protected EnemyMelee enemyMelee;
    protected EnemyRange enemyRange;

    public EnemyState(Enemy enemy, StateMachine<EntityState> stateMachine, Projectile_Base projectile, string animBoolName) : base(stateMachine, projectile, animBoolName)
    {
        enemyMelee = enemy as EnemyMelee;
        enemyRange = enemy as EnemyRange;
        anim = enemy.anim;
        col = enemy.col;
    }

    public override void Update()
    {
        base.Update();


        bool frozen = enemyMelee.stateHandler != null && enemyMelee.stateHandler.IsFrozen();

        if (frozen)
        {
            stateMachine.ChangeState(enemyMelee.idleState);
            return;
        }
    }
}
