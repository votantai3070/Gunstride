public class EnemyMelee : Enemy
{
    public Enemy_Combat combat { get; private set; }

    public Enemy_IdleState idleState { get; private set; }
    public EnemyMelee_AttackState attackState { get; private set; }
    public Enemy_DeadState deadState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        combat = GetComponent<Enemy_Combat>();
    }

    protected override void Start()
    {
        base.Start();


        idleState = new Enemy_IdleState(this, stateMachine, projectile, "Idle");
        attackState = new EnemyMelee_AttackState(this, stateMachine, projectile, "Attack");
        deadState = new Enemy_DeadState(this, stateMachine, projectile, "Dead");

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public override void TryToDeadState()
    {
        stateMachine.ChangeState(deadState);
    }
}
