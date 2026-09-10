using System.Collections.Generic;
using UnityEngine;

public class ShockwaveDamage2D : MonoBehaviour
{
    [SerializeField] private ParticleSystem shockwaveParticle;

    [Header("Shockwave")]
    [SerializeField] private CircleCollider2D damageCollider;
    [SerializeField] private float duration = 0.35f;
    [SerializeField] private float maxRadius = 3f;

    [Header("Damage")]
    [SerializeField] private int damage = 20;
    [SerializeField] private LayerMask enemyLayer;

    private readonly HashSet<IDamageable> hitTargets = new();

    private float timer;
    private float startRadius;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (damageCollider == null)
            damageCollider = GetComponent<CircleCollider2D>();

        if (damageCollider != null)
            damageCollider.isTrigger = true;

        if (shockwaveParticle == null)
            shockwaveParticle = GetComponentInChildren<ParticleSystem>();
    }

    private void OnEnable()
    {
        timer = 0f;

        hitTargets.Clear();

        if (damageCollider != null)
        {
            startRadius = 0.05f;
            damageCollider.radius = startRadius;
        }
    }

    private void Update()
    {
        if (damageCollider == null)
            return;

        timer += Time.deltaTime;

        float t = Mathf.Clamp01(timer / duration);

        damageCollider.radius = Mathf.Lerp(startRadius, maxRadius, t);

        if (timer >= duration)
        {
            AutomaticDespawnObject();
        }
    }

    public void SetupExplode(int damage, LayerMask enemyLayer)
    {
        if (shockwaveParticle != null)
            duration = shockwaveParticle.main.duration / 2f;

        this.damage = damage;
        this.enemyLayer = enemyLayer;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((enemyLayer.value & (1 << other.gameObject.layer)) == 0)
            return;

        if (!other.TryGetComponent(out IDamageable damageable))
            return;

        if (!hitTargets.Add(damageable))
            return;


        damageable.TakeDamage(damage);
    }

    protected void AutomaticDespawnObject()
    {
        ObjectPool.Instance.Despawn(gameObject);
    }
}