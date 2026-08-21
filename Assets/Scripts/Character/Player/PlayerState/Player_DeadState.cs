public class Player_DeadState : PlayerState
{
    public Player_DeadState(Player player, StateMachine<EntityState> stateMachine, Projectile_Base projectile, string animBoolName) : base(player, stateMachine, projectile, animBoolName)
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
    }
}
