public class EnemyState : EntityState
{
    protected Enemy enemy;

    public EnemyState(Enemy enemy, StateMachine<EntityState> stateMachine, Projectile_Base projectile, string animBoolName) : base(stateMachine, projectile, animBoolName)
    {
        this.enemy = enemy;
        anim = enemy.anim;
        col = enemy.col;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.FlippedLeft();
    }
}
