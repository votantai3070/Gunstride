public class Player : Entity
{
    public PlayerSkillManager skillManager { get; private set; }
    public Player_Combat combat { get; private set; }
    public PlayerInputMobile input { get; private set; }

    public Player_IdleState idleState { get; private set; }
    public Player_RunState runState { get; private set; }
    public Player_DeadState deadState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        skillManager = GetComponentInChildren<PlayerSkillManager>();
        combat = GetComponent<Player_Combat>();
        input = GetComponent<PlayerInputMobile>();

        idleState = new Player_IdleState(this, stateMachine, "Idle");
        runState = new Player_RunState(this, stateMachine, "Run");
        deadState = new Player_DeadState(this, stateMachine, "Dead");
    }

    protected override void Start()
    {
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }
}
