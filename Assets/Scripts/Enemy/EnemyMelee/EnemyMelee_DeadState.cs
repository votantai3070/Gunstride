using UnityEngine;

public class EnemyMelee_DeadState : EnemyState
{
    float deadAnim;

    public EnemyMelee_DeadState(Enemy enemy, StateMachine<EntityState> stateMachine, Projectile_Base projectile, string animBoolName) : base(enemy, stateMachine, projectile, animBoolName)
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

        Debug.Log("Change Idle state");
    }

    public override void Update()
    {
        base.Update();

        deadAnim -= Time.deltaTime;
        if (deadAnim <= 0)
        {
            deadAnim = 0;
            stateMachine.ChangeState(enemyMelee.idleState);
            ObjectPool.instance.Despawn(enemyMelee.gameObject);
        }

    }
}
