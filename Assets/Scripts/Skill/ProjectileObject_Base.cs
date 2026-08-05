using System.Collections.Generic;
using UnityEngine;

public class ProjectileObject_Base : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Collider2D col;
    protected VFX_AutomationEffect vfx;

    protected readonly HashSet<Collider2D> hitTargets = new();
    [SerializeField] protected List<GameObject> hitEffectGos = new();
    public List<SkillBuffDataSO> activeBuffs = new();

    protected IProjectileUpgrade[] upgrades;
    protected Projectile_Base projectileManager;

    [SerializeField] protected float speed;
    [SerializeField] protected int damage;
    [SerializeField] protected LayerMask whatIsTarget;
    [SerializeField] protected float attackCooldown = .1f;

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
    }

    protected virtual void SetupProjectile()
    {
        for (int i = 0; i < upgrades.Length; i++)
        {
            SkillBuffDataSO buff = activeBuffs.Find(b => b.upgradeType == upgrades[i].upgradeType);
            if (buff != null)
            {
                upgrades[i].Initialize(this, buff);
                hitEffectGos.Add(buff.hitEffect);
            }
        }

        vfx?.SetupEffectGo(hitEffectGos, .5f);
    }

    private void Update()
    {
        rb.linearVelocity = new Vector2(faceDir * speed, 0);
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

            for (int i = 0; i < upgrades.Length; i++)
            {
                if (upgrades[i].upgradeType == activeBuffs[i].upgradeType)
                    upgrades[i].OnHit(target);
            }

            bool shouldDespawn = true;
            for (int i = 0; i < upgrades.Length; i++)
            {
                if (upgrades[i].upgradeType == activeBuffs[i].upgradeType)
                    shouldDespawn &= upgrades[i].ShouldDespawn;
            }

            if (shouldDespawn)
                ObjectPool.Instance.Despawn(gameObject);
        }

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