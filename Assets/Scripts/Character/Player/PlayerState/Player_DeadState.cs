using UnityEngine;

public class Player_DeadState : PlayerState
{
    float deadAnim;

    public Player_DeadState(Player player, StateMachine<EntityState> stateMachine, Projectile_Base projectile, string animBoolName) : base(player, stateMachine, projectile, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        deadAnim = anim.GetCurrentAnimatorStateInfo(0).length;
    }

    public override void Exit()
    {
        base.Exit();

        UI.Instance.OpenTotalSummaryUI();
    }

    public override void Update()
    {
        base.Update();

        deadAnim -= Time.deltaTime;
        if (deadAnim <= 0)
        {
            deadAnim = 0;
            stateMachine.ChangeState(player.idleState);
        }
    }
}
