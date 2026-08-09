using System.Collections.Generic;
using UnityEngine;

public class ProjectileObject_Base : MonoBehaviour
{
    public Rigidbody2D rb { get; set; }
    protected Collider2D col;
    protected VFX_AutomationEffect vfx;

    protected readonly HashSet<Collider2D> hitTargets = new();
    [SerializeField] protected List<GameObject> hitEffectGos = new();
    public List<SkillBuffDataSO> activeBuffs = new();

    protected IProjectileUpgrade[] upgrades;
    protected SkillUpgradeType upgradeType = SkillUpgradeType.None;
    protected Projectile_Base projectileManager;

    [SerializeField] protected float speed;
    [SerializeField] protected int damage;
    [SerializeField] protected LayerMask whatIsTarget;
    [SerializeField] protected float attackCooldown = .1f;
    public int bounceCount;
    public int pierceCount;

    protected Vector2 moveDirection;
    protected float lastAttack;
    protected float faceDir;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        if (GetComponent<VFX_AutomationEffect>() == null)
            gameObject.AddComponent<VFX_AutomationEffect>();

        vfx = GetComponent<VFX_AutomationEffect>();
        upgrades = GetComponents<IProjectileUpgrade>();
    }

    protected virtual void OnEnable()
    {
        lastAttack = -999f;
        hitTargets.Clear();
        rb.linearVelocity = Vector2.zero;
        moveDirection = Vector2.zero;
    }

    protected virtual void SetupProjectile()
    {
        hitEffectGos.Clear();

        for (int i = 0; i < upgrades.Length; i++)
        {
            SkillBuffDataSO buff = activeBuffs.Find(b => b.upgradeType == upgrades[i].upgradeType);
            if (buff != null)
            {
                upgrades[i].Initialize(this, buff);
                if (buff.hitEffect != null)
                    hitEffectGos.Add(buff.hitEffect);
            }
        }

        vfx?.SetupEffectGo(hitEffectGos, .5f);
    }

    private void FixedUpdate()
    {
        if (moveDirection.sqrMagnitude <= 0.0001f)
            return;

        rb.linearVelocity = moveDirection * speed;
    }

    protected virtual void Attack(Collider2D target)
    {
        if (target == null) return;
        if (hitTargets.Contains(target)) return;
        if (!CanAttack()) return;

        IDamageable damageable = target.GetComponent<IDamageable>();
        if (damageable == null) return;

        lastAttack = Time.time;
        hitTargets.Add(target);

        if (damageable.TakeDamage(damage))
        {
            if (vfx != null)
                vfx.CreateEffect(target.transform);

            bool shouldDespawn = true;

            foreach (var upgrade in upgrades)
            {
                if (!HasUpgrade(upgrade.upgradeType))
                    continue;

                upgrade.OnHit(target);
                shouldDespawn &= upgrade.ShouldDespawn;
            }

            if (shouldDespawn)
            {
                ObjectPool.Instance.Despawn(gameObject);
            }
        }

    }

    public void SetDirection(Vector2 direction)
    {
        if (direction.sqrMagnitude <= 0.0001f)
            return;

        moveDirection = direction.normalized;
        rb.linearVelocity = moveDirection * speed;

        if (Mathf.Abs(direction.x) > 0.01f)
        {
            faceDir = Mathf.Sign(direction.x);

            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * faceDir;
            transform.localScale = scale;
        }
    }

    public bool HasHitTarget(Collider2D target)
    {
        return target != null && hitTargets.Contains(target);
    }

    public bool HasUpgrade(SkillUpgradeType type)
    {
        return (upgradeType & type) == type;
    }

    protected bool CanAttack()
    {
        return Time.time > lastAttack + attackCooldown;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Attack(collision);
    }
}