public class PlayerState : EntityState
{
    protected Player player;

    public PlayerState(Player player, StateMachine<EntityState> stateMachine, Projectile_Base projectile, string animBoolName) : base(stateMachine, projectile, animBoolName)
    {
        this.player = player;
        anim = player.anim;
        col = player.col;
    }
}
