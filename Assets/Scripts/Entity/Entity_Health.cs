using System;
using UnityEngine;

public class Entity_Health : MonoBehaviour, IDamageable, IHealable
{
    public Action<float, float> OnHealthChanged;

    protected Entity entity;

    [SerializeField] protected float currentHealth = 0;
    [SerializeField] protected float maxHealth;

    public float CurrentHealth => currentHealth;

    protected virtual void Awake()
    {
        entity = GetComponent<Entity>();

        maxHealth = entity.characterData.maxHealth;
    }

    protected virtual void OnEnable()
    {
        currentHealth = maxHealth;
    }

    protected virtual void OnDisable() { }

    protected virtual void Start() { }

    public void IncreaseHealth(float health)
    {
        currentHealth = Mathf.Clamp(currentHealth + health, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void DecreaseHealth(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public virtual bool TakeDamage(int damage)
    {
        if (currentHealth == 0) return false;

        DecreaseHealth(damage);

        if (currentHealth <= 0)
            Dead();

        return true;
    }

    private void Dead()
    {
        entity.TryToDeadState();
    }

}
