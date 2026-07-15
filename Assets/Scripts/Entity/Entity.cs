using UnityEngine;

public class Entity : MonoBehaviour
{
    private Entity_Combat entityCombat;

    protected StateMachine<EntityState> stateMachine;
    public Animator anim;
    public Collider2D col;

    [Header("Detected System")]
    [SerializeField] private LayerMask whatIsTarget;
    [SerializeField] private float detectDistance;

    [Space]
    protected bool flipped;

    protected virtual void Awake()
    {
        entityCombat = GetComponent<Entity_Combat>();

        stateMachine = new StateMachine<EntityState>();
        anim = GetComponentInChildren<Animator>();
        col = GetComponent<Collider2D>();
    }

    protected virtual void Start() { }
    protected virtual void Update()
    {
        Debug.Log("Detected Target: " + DetectedTarget());
    }

    public virtual bool DetectedTarget()
    {
        Vector2 direction = flipped ? Vector2.left : Vector2.right;
        Vector2 origin = (Vector2)transform.position + direction * Vector2.up;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, detectDistance, whatIsTarget);

        Debug.DrawRay(origin, direction * detectDistance, Color.red);

        return hit.collider;
    }

    public bool IsFlipped() => flipped;
}
