using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Utils utils = new();

    protected Entity_Health entityHealth;
    public Entity_Combat entityCombat { get; private set; }
    public EntitySkillManager entitySkillManager { get; private set; }
    protected Projectile_Base projectile;

    public Entity_Effects entityEffects { get; private set; }
    public Entity_StatusHandler stateHandler { get; private set; }

    protected StateMachine<EntityState> stateMachine;

    public Animator anim { get; private set; }
    public Collider2D col { get; private set; }
    public Rigidbody2D rb { get; private set; }

    [Header("Detection")]
    public LayerMask whatIsTarget;
    public float detectDistance = 6f;
    public float attackDistance = 1.5f;
    [SerializeField] protected float rayOriginYOffset = 1f;

    [Header("Character Setup")]
    public CharacterDataSO characterData;
    public float idleTime = 3f;
    public float speed = 5f;

    protected float moveSpeedMultiplier = 1f;

    protected Coroutine elementalEffectCo;

    [Space]
    [SerializeField] protected bool flipped;
    public bool isTrigger { get; set; }
    public bool isAttack { get; set; }

    protected virtual void Awake()
    {
        entityCombat = GetComponent<Entity_Combat>();
        entityHealth = GetComponent<Entity_Health>();
        entityEffects = GetComponent<Entity_Effects>();
        stateHandler = GetComponent<Entity_StatusHandler>();
        entitySkillManager = GetComponentInChildren<EntitySkillManager>();

        stateMachine = new StateMachine<EntityState>();

        anim = GetComponentInChildren<Animator>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        if (entitySkillManager != null && characterData != null && characterData.skillData != null)
        {
            projectile = entitySkillManager.GetSkillByType(characterData.skillData.skillType);
        }
    }

    protected virtual void OnEnable()
    {
        if (characterData != null)
            speed = characterData.speed;
    }

    protected virtual void Update()
    {

    }

    public void SlowDown(float duration)
    {
        if (elementalEffectCo != null)
            StopCoroutine(elementalEffectCo);

        elementalEffectCo = StartCoroutine(SlowDownCo(duration));
    }

    public virtual void StopSlowDown()
    {
        StopCoroutine(elementalEffectCo);
    }

    protected virtual IEnumerator SlowDownCo(float duration)
    {
        yield return null;
    }

    public void SetMoveSpeedMultiplier(float multiplier)
    {
        moveSpeedMultiplier = Mathf.Clamp01(multiplier);
    }

    public void ResetMoveSpeedMultiplier()
    {
        moveSpeedMultiplier = 1f;
    }

    public virtual bool CanDetectTarget()
    {
        return HasTargetInRange(detectDistance);
    }

    public virtual bool CanAttackTarget()
    {
        return HasTargetInRange(attackDistance);
    }

    protected virtual bool HasTargetInRange(float distance)
    {
        Vector2 origin = GetRayOrigin();
        Vector2 direction = GetFacingDirection();

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, whatIsTarget);
        Debug.DrawRay(origin, direction * distance, hit.collider ? Color.green : Color.red);

        return hit.collider != null;
    }

    protected Vector2 GetRayOrigin()
    {
        return (Vector2)transform.position + Vector2.up * rayOriginYOffset;
    }

    protected Vector2 GetFacingDirection()
    {
        return flipped ? Vector2.left : Vector2.right;
    }

    public void SetVelocity(float moveSpeed)
    {
        if (rb == null)
            return;

        float dir = flipped ? -1f : 1f;
        rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y);
    }

    public virtual void TryToDeadState() { }

    public bool IsFlipped() => flipped;

    protected virtual void OnDrawGizmosSelected()
    {
        Vector3 origin = transform.position + Vector3.up * rayOriginYOffset;
        Vector3 direction = flipped ? Vector3.left : Vector3.right;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(origin, origin + direction * detectDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(origin, origin + direction * attackDistance);
    }
}