public class Enemy : Entity
{
    public Enemy_IdleState idleState { get; private set; }


    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(this, stateMachine, "Idle");
    }

    protected override void Start()
    {
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        stateMachine.currentState.Update();
    }

    public void FlipX()
    {
        if (flipped) return;

        flipped = true;
        transform.Rotate(0, 180, 0);
    }
}
