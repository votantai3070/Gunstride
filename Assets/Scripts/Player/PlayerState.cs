public class PlayerState : EntityState
{
    protected Player player;

    public PlayerState(Player player, StateMachine<EntityState> stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.player = player;
        anim = player.anim;
        col = player.col;
    }
}
