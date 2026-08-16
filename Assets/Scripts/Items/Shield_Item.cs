using System.Collections;
using UnityEngine;

public class Shield_Item : Pickup_Item
{
    [SerializeField] private float shieldDuration;
    private Coroutine immuneDamagedCo;

    public override void Pickup(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Entity_Effects effects = collider.GetComponent<Entity_Effects>();
            Player_Health playerHealth = collider.GetComponent<Player_Health>();

            effects.CreateShield(collider.transform, shieldDuration);
            ImmuneDamaged(playerHealth, shieldDuration);
        }
    }

    private void ImmuneDamaged(Player_Health playerHealth, float duration)
    {
        if (playerHealth == null)
            return;

        if (immuneDamagedCo != null)
            StopCoroutine(immuneDamagedCo);

        immuneDamagedCo = StartCoroutine(ImmuneDamagedCo(playerHealth, shieldDuration));
    }

    private IEnumerator ImmuneDamagedCo(Player_Health playerHealth, float duration)
    {
        playerHealth.IsDamaged(true);
        yield return new WaitForSeconds(duration);
        playerHealth.IsDamaged(false);
    }
}
