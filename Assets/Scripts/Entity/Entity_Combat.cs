using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    protected Entity entity;
    [SerializeField] private ElementalEffectData effectData;

    [Header("Attack Settings")]
    protected LayerMask whatIsTarget;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackCooldown = 0.25f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 1f;

    private float lastAttackTime;

    private void Awake()
    {
        entity = GetComponent<Entity>();
    }

    private void Start()
    {
        whatIsTarget = entity.whatIsTarget;
    }

    public ElementData GetElementData()
    {
        return new(entity.entitySkillManager, entity.characterData.skillData.skillType);
    }

    public ElementalEffectData GetEffectData() => effectData;


    public void TryAttack()
    {
        Debug.Log("Attack");

        if (Time.time < lastAttackTime + attackCooldown)
            return;

        Collider2D target = TargetToAttack();
        if (target == null)
            return;

        lastAttackTime = Time.time;
        Attack(target.gameObject);
    }

    private void Attack(GameObject target)
    {
        if (target == null)
            return;

        if (target.TryGetComponent<IDamageable>(out var damageable))
        {
            bool canHit = damageable.TakeDamage(attackDamage);

            if (canHit)
                Debug.Log("Attack");
        }
    }

    private Collider2D TargetToAttack()
    {
        Collider2D target = null;
        float minSqrDistance = Mathf.Infinity;

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsTarget);

        foreach (var hit in hits)
        {
            if (hit.gameObject == gameObject)
                continue;

            float sqrDistance = ((Vector2)hit.transform.position - (Vector2)transform.position).sqrMagnitude;

            if (sqrDistance < minSqrDistance)
            {
                minSqrDistance = sqrDistance;
                target = hit;
            }
        }

        return target;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}