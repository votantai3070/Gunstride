public class Player : Entity
{
    public PlayerSkillManager skillManager { get; private set; }
    public PlayerLaneMovement movement { get; private set; }
    public PlayerInputMobile input { get; private set; }
    public Player_Health health { get; private set; }

    public Player_IdleState idleState { get; private set; }
    public Player_RunState runState { get; private set; }
    public Player_DeadState deadState { get; private set; }


    protected override void Awake()
    {
        base.Awake();

        skillManager = GetComponentInChildren<PlayerSkillManager>();
        movement = GetComponent<PlayerLaneMovement>();
        input = GetComponent<PlayerInputMobile>();
        health = GetComponent<Player_Health>();

        UI.Instance.SetPlayer(this);
    }

    protected override void Start()
    {
        idleState = new Player_IdleState(this, stateMachine, projectile, "Idle");
        runState = new Player_RunState(this, stateMachine, projectile, "Run");
        deadState = new Player_DeadState(this, stateMachine, projectile, "Dead");


        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        stateMachine.currentState.Update();
    }
}
