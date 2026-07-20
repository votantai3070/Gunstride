using System;
using UnityEngine;

public class Entity_Health : MonoBehaviour, IDamageable
{
    public Action<float, float> OnHealthChanged;

    protected Entity entity;

    public float currentHealth = 0;
    public float maxHealth;

    protected virtual void Awake()
    {
        entity = GetComponent<Entity>();

        maxHealth = entity.characterData.maxHealth;
    }

    protected virtual void OnEnable()
    {
        currentHealth = maxHealth;
    }

    protected virtual void OnDisable()
    {

    }

    protected virtual void Start()
    {
    }

    public virtual bool TakeDamage(int damage)
    {
        if (currentHealth == 0) return false;

        currentHealth -= damage;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
            Dead();

        return true;
    }

    private void Dead()
    {
        entity.TryToDeadState();
    }
}
