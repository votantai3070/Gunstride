using System;
using UnityEngine;

public class Entity_Health : MonoBehaviour, IDamageable, IHealable
{
    public Action<float, float> OnHealthChanged;

    protected Entity entity;

    [SerializeField] protected float currentHealth;
    [SerializeField] protected float maxHealth = 1f;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    protected virtual void Awake()
    {
        entity = GetComponent<Entity>();

        if (entity == null)
        {
            Debug.LogError(
                $"Entity_Health needs an Entity component on {name}.",
                this
            );
        }
    }

    protected virtual void OnEnable()
    {
    }

    protected virtual void OnDisable()
    {

    }

    protected virtual void Start()
    {
    }

    public void InitializeHealth()
    {
        if (entity == null)
        {
            Debug.LogError($"Cannot initialize health: Entity is null on {name}.", this);
            return;
        }

        if (entity.EntityStats != null)
        {
            maxHealth = entity.EntityStats.maxHealth.GetValue();
        }
        else if (entity.characterData != null)
        {
            maxHealth = entity.characterData.maxHealth;
        }
        else
        {
            Debug.LogError($"No EntityStats or characterData found on {name}.", this);
            maxHealth = 1f;
        }

        maxHealth = Mathf.Max(1f, maxHealth);
        currentHealth = maxHealth;

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void IncreaseHealth(float health)
    {
        currentHealth = Mathf.Clamp(currentHealth + health, 0f, maxHealth);

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void DecreaseHealth(float damage)
    {
        currentHealth = Mathf.Clamp(
            currentHealth - damage,
            0f,
            maxHealth
        );

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public virtual bool TakeDamage(int damage, bool isCrit)
    {
        if (currentHealth <= 0f)
            return false;

        DecreaseHealth(damage);

        if (currentHealth <= 0f)
            Dead();

        return true;
    }

    protected virtual void Dead()
    {
        entity.TryToDeadState();
    }
}