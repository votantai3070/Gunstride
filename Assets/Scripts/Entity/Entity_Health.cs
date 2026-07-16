using UnityEngine;

public class Entity_Health : MonoBehaviour, IDamageable
{
    private float currentHealth = 0;

    public virtual bool TakeDamage(int damage)
    {
        //if (currentHealth == 0) return false;

        //currentHealth -= damage;

        //if (currentHealth <= 0)
        //{
        //    Dead();
        //}

        return true;
    }

    private void Dead()
    {

    }
}
