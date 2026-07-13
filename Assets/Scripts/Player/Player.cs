using UnityEngine;

public class Player : MonoBehaviour
{
    private StateMachine<EntityState> stateMachine;
    public Animator anim;
    public Collider2D col;

    public Player_IdleState idleState { get; private set; }
    public Player_RunState runState { get; private set; }
    public Player_DeadState deadState { get; private set; }

    private void Awake()
    {
        stateMachine = new StateMachine<EntityState>();
        anim = GetComponentInChildren<Animator>();
        col = GetComponent<Collider2D>();

        idleState = new Player_IdleState(this, stateMachine, "Idle");
        runState = new Player_RunState(this, stateMachine, "Run");
        deadState = new Player_DeadState(this, stateMachine, "Dead");
    }

    protected void Start()
    {
        stateMachine.Initialize(idleState);
    }

    protected void Update()
    {
        stateMachine.currentState.Update();
    }
}
