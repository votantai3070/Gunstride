public class Player_IdleState : PlayerState
{
    public Player_IdleState(Player player, StateMachine<EntityState> stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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

        if (GameManager.Instance.IsGameStarted())
        {
            stateMachine.ChangeState(player.runState);
        }
    }
}
