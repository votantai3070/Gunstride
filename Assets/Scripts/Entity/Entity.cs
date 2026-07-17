using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Utils utils = new();
    protected Entity_Health entityHealth;
    private Entity_Combat entityCombat;
    public Entity_Effects entityEffects { get; private set; }
    private EntitySkillManager entitySkillManager;

    protected StateMachine<EntityState> stateMachine;
    public Animator anim { get; private set; }
    public Collider2D col { get; private set; }

    [Header("Detected System")]
    public LayerMask whatIsTarget;
    [SerializeField] private float detectDistance;

    [Header("Character Setup")]
    public CharacterDataSO characterData;
    protected Projectile_Base projectile;
    public float idleTime = 3;

    [Space]
    protected bool flipped;
    public bool isTrigger { get; set; }
    public bool isAttack { get; set; }

    protected virtual void Awake()
    {
        entityCombat = GetComponent<Entity_Combat>();
        entityHealth = GetComponent<Entity_Health>();
        entityEffects = GetComponent<Entity_Effects>();
        entitySkillManager = GetComponentInChildren<EntitySkillManager>();

        stateMachine = new StateMachine<EntityState>();
        anim = GetComponentInChildren<Animator>();
        col = GetComponent<Collider2D>();
    }

    protected virtual void Start()
    {
        if (entitySkillManager != null)
            projectile = entitySkillManager.GetSkill(characterData.skillData.skillType);
    }

    protected virtual void Update() { }

    public virtual bool DetectedTarget()
    {
        Vector2 direction = flipped ? Vector2.left : Vector2.right;
        Vector2 origin = (Vector2)transform.position + direction * Vector2.up;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, detectDistance, whatIsTarget);

        Debug.DrawRay(origin, direction * detectDistance, Color.red);

        return hit.collider;
    }

    public virtual void TryToDeadState() { }

    public bool IsFlipped() => flipped;

    private void OnDrawGizmos()
    {
        Vector3 origin = transform.position;
        Vector3 direction = flipped ? Vector2.left : Vector2.right;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(origin, origin + direction * detectDistance);
    }
}
