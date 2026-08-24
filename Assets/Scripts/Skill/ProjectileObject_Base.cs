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

    [Header("Element Settings")]
    protected IProjectileUpgrade[] upgrades;
    protected SkillUpgradeType upgradeType = SkillUpgradeType.None;
    protected Projectile_Base projectileManager;
    protected VFX_AutomaticDespawn[] despawnVfx;
    [SerializeField] protected ElementType elementType;
    [SerializeField] protected ElementalEffectData elementEffectData;

    [Header("Projectile Setup")]
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

        despawnVfx = GetComponentsInChildren<VFX_AutomaticDespawn>();
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

        if (!target.TryGetComponent<IDamageable>(out var damageable)) return;

        lastAttack = Time.time;
        hitTargets.Add(target);

        if (damageable.TakeDamage(damage))
        {
            StatusIconBarUI iconBarUI = target.GetComponentInChildren<StatusIconBarUI>();
            Entity entity = target.GetComponent<Entity>();

            if (target.TryGetComponent<Entity_StatusHandler>(out var statusHandler))
                statusHandler.ApplyStatusEffect(elementType, elementEffectData, iconBarUI, entity);

            if (vfx != null)
                vfx.CreateEffect(target.transform);

            if (target.TryGetComponent<Entity_Effects>(out var effects))
                GetElementVfx(effects);

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
                if (despawnVfx.Length > 0)
                {
                    foreach (var vfx in despawnVfx)
                    {
                        if (vfx.gameObject.activeSelf)
                            vfx.DespawnObject();
                    }
                }

                ObjectPool.Instance.Despawn(gameObject);
            }
        }

    }

    private void GetElementVfx(Entity_Effects effects)
    {
        if (elementType == ElementType.Ice)
            effects.GetElementVfx(elementEffectData.chillDuration, elementType);
        else if (elementType == ElementType.Lightning)
            effects.GetElementVfx(elementEffectData.lightningThunderDuration, elementType);
        else if (elementType == ElementType.Fire)
            effects.GetElementVfx(elementEffectData.burnDuration, elementType);
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