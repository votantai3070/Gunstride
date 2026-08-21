using UnityEngine;

public class Animal : Entity
{
    [Header("Animal Settings")]
    [SerializeField] private float detectedRadius;
    [SerializeField] private float attackRadius;
    [SerializeField] private float patrolRadius;
    [SerializeField] private float patrolSpeed;

    [Header("Patrol Points")]
    [SerializeField] private Transform[] patrolPoints;

    private Vector3[] patrolPointsPos;
    private int currentPoint;

    public Animal_IdleState idleState { get; private set; }
    public Animal_WalkState walkState { get; private set; }
    public Animal_DeadState deadState { get; private set; }
    public Animal_HurtState hurtState { get; private set; }
    public Animal_HuntingState huntState { get; private set; }
    public Animal_PrepareHuntState prepareHuntState { get; private set; }
    public Animal_RelaxState relaxState { get; private set; }
    public Animal_JumpState jumpState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

    }

    protected override void Start()
    {
        base.Start();


        idleState = new Animal_IdleState(this, stateMachine, projectile, "Idle");
        walkState = new Animal_WalkState(this, stateMachine, projectile, "Walk");
        deadState = new Animal_DeadState(this, stateMachine, projectile, "Dead");
        hurtState = new Animal_HurtState(this, stateMachine, projectile, "Hurt");
        huntState = new Animal_HuntingState(this, stateMachine, projectile, "Hunting");
        prepareHuntState = new Animal_PrepareHuntState(this, stateMachine, projectile, "PrepareHunt");
        relaxState = new Animal_RelaxState(this, stateMachine, projectile, "Relax");
        jumpState = new Animal_JumpState(this, stateMachine, projectile, "Jump");

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        stateMachine.currentState.Update();
    }

    protected override void OnEnable()
    {
    }

    protected override void OnDisable()
    {

    }

    public void Patrol()
    {
        if (patrolPointsPos == null || patrolPointsPos.Length == 0)
        {
            StopMovement();
            return;
        }

        if (currentPoint < 0 || currentPoint >= patrolPointsPos.Length)
        {
            StopMovement();
            return;
        }

        Vector3 target = patrolPointsPos[currentPoint];

        if (target == null)
        {
            StopMovement();
            SelectNextPatrolPoint();
            return;
        }

        SetVelocity(patrolSpeed);
    }

    public void SetupAnimal()
    {
        patrolPointsPos = SetupPatrolPoints();
    }

    private Vector3[] SetupPatrolPoints()
    {
        Vector3[] patrols = new Vector3[patrolPoints.Length];

        for (int i = 0; i < patrolPoints.Length; i++)
        {
            patrols[i] = patrolPoints[i].position;
            patrolPoints[i].gameObject.SetActive(false);
        }

        return patrols;
    }

    public override void SetVelocity(float speed)
    {
        if (rb == null)
            return;

        Vector2 targetPosition = patrolPointsPos[currentPoint];

        Vector2 direction = targetPosition - (Vector2)transform.position;

        if (direction.sqrMagnitude <= 0.0001f)
        {
            StopMovement();
            return;
        }

        rb.linearVelocity = direction.normalized * speed;

        Debug.Log("Direction: " + direction);

        if (Mathf.Abs(direction.x) > 0.01f)
        {
            flipped = direction.x < 0f;
            Debug.Log("Flipped: " + flipped);
            utils.Flipped(flipped, transform);
        }
    }

    public bool IsAtPatrolPoint()
    {
        if (patrolPointsPos == null || patrolPointsPos.Length == 0)
            return true;

        if (currentPoint < 0 || currentPoint >= patrolPointsPos.Length)
            return true;

        Vector3 target = patrolPointsPos[currentPoint];

        if (target == null)
            return true;

        return Vector2.Distance(rb.position, target) <= 0.05f;
    }

    public void SelectNextPatrolPoint()
    {
        if (patrolPointsPos == null || patrolPointsPos.Length == 0)
            return;

        currentPoint = Random.Range(0, patrolPointsPos.Length);
    }

    public Vector3 GetPatrolPosition()
    {
        if (patrolPointsPos == null || patrolPointsPos.Length == 0)
            return transform.position;

        if (currentPoint < 0 || currentPoint >= patrolPointsPos.Length)
            return transform.position;

        Vector3 target = patrolPointsPos[currentPoint];

        return target != null ? target : transform.position;
    }

    public void StopMovement()
    {
        if (rb == null)
            return;

        rb.linearVelocity = Vector2.zero;
    }

    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectedRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}