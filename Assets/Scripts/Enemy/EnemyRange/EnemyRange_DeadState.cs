using UnityEngine;

public class EnemyRange_DeadState : EnemyState
{
    float deadAnim;

    public EnemyRange_DeadState(Enemy enemy, StateMachine<EntityState> stateMachine, Projectile_Base projectile, string animBoolName) : base(enemy, stateMachine, projectile, animBoolName)
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
    }

    public override void Update()
    {
        base.Update();

        deadAnim -= Time.deltaTime;
        if (deadAnim <= 0)
        {
            deadAnim = 0;
            stateMachine.ChangeState(enemyRange.idleState);
            ObjectPool.instance.Despawn(enemyRange.gameObject);
        }

    }
}
