using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    protected Entity entity;
    [SerializeField] private ElementDataScale effectData;

    [Header("Element Settings")]
    [SerializeField] private ElementType currentElement;

    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackCooldown = 0.25f;
    [SerializeField] private float attackRadius = 1f;

    [Header("Weapon Data")]
    [SerializeField] protected Transform equipWeaponPoint;
    [SerializeField] protected WeaponDataSO weaponData;
    [SerializeField] protected Weapon weapon;
    protected LayerMask whatIsTarget;

    private float lastAttackTime;

    protected virtual void Awake()
    {
        entity = GetComponent<Entity>();
    }

    protected virtual void Start()
    {
        whatIsTarget = entity.whatIsTarget;
        weapon ??= new Weapon(weaponData);
    }

    protected virtual void OnValidate()
    {
        if (weaponData != null)
            weapon = new Weapon(weaponData);
    }

    public ElementType GetCurrentElementType() => currentElement;

    public void SetElement(ElementType type) => currentElement = type;

    public ElementalEffectData GetElementalEffectData()
    {
        return new(effectData);
    }

    public void TryAttack()
    {
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
        }
    }

    private Collider2D TargetToAttack()
    {
        Collider2D target = null;
        float minSqrDistance = Mathf.Infinity;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRadius, whatIsTarget);

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
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}