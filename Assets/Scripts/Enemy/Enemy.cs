public class Enemy : Entity
{
    public EnemySkillManager skillManager { get; private set; }

    public Enemy_IdleState idleState { get; private set; }
    public Enemy_AttackState attackState { get; private set; }


    protected override void Awake()
    {
        base.Awake();

        skillManager = GetComponentInChildren<EnemySkillManager>();
    }

    protected override void Start()
    {
        base.Start();

        idleState = new Enemy_IdleState(this, stateMachine, projectile, "Idle");
        attackState = new Enemy_AttackState(this, stateMachine, projectile, "Attack");

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public void FlippedLeft()
    {
        if (flipped) return;

        flipped = true;
        utils.FlipLeft(transform);
    }
}
