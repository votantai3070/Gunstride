using Managers;
using UnityEngine;

public class EntityState : IState
{
    protected StateMachine<EntityState> stateMachine;
    protected Projectile_Base projectile;
    protected string animBoolName;

    //protected Rigidbody2D rb;
    protected Animator anim;
    protected Collider2D col;

    protected float stateTimer;

    public EntityState(StateMachine<EntityState> stateMachine, Projectile_Base projectile, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        this.projectile = projectile;
    }

    public virtual void Enter()
    {
        anim.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        anim.SetBool(animBoolName, false);
    }

    public virtual void Update()
    {
        if (GameManager.Instance.IsGameStarted())
            stateTimer -= Time.deltaTime;
    }
}
