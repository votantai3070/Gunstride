public class EnemyRange : Enemy
{
    public EnemyRange_IdleState idleState { get; private set; }
    public EnemyRange_AttackState attackState { get; private set; }
    public EnemyRange_DeadState deadState { get; private set; }

    protected override void Start()
    {
        base.Start();

        idleState = new EnemyRange_IdleState(this, stateMachine, projectile, "Idle");
        attackState = new EnemyRange_AttackState(this, stateMachine, projectile, "Attack");
        deadState = new EnemyRange_DeadState(this, stateMachine, projectile, "Dead");

        stateMachine.Initialize(idleState);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        stateMachine.ChangeState(idleState);
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
