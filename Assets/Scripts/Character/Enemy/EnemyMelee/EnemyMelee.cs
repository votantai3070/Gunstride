
using UnityEngine;

public enum EnemyMeleeType { Idle, Run, Shield }

public class EnemyMelee : Enemy
{
    public Enemy_Combat combat { get; private set; }
    public EnemyMelee_IdleState idleState { get; private set; }
    public EnemyMelee_AttackState attackState { get; private set; }
    public EnemyMelee_DeadState deadState { get; private set; }
    public EnemyMelee_WalkState walkState { get; private set; }
    public EnemyMelee_BlockState blockState { get; private set; }
    public EnemyMelee_HurtState hurtState { get; private set; }

    [Header("Enemy Melee Setup")]
    public EnemyMeleeType meleeType;

    protected override void Awake()
    {
        base.Awake();
        combat = GetComponent<Enemy_Combat>();
    }

    protected override void Start()
    {
        base.Start();

        idleState = new EnemyMelee_IdleState(this, stateMachine, projectile, "Idle");
        attackState = new EnemyMelee_AttackState(this, stateMachine, projectile, "Attack");
        deadState = new EnemyMelee_DeadState(this, stateMachine, projectile, "Dead");
        walkState = new EnemyMelee_WalkState(this, stateMachine, projectile, "Walk");
        blockState = new EnemyMelee_BlockState(this, stateMachine, projectile, "Block");
        hurtState = new EnemyMelee_HurtState(this, stateMachine, projectile, "Hurt");

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
