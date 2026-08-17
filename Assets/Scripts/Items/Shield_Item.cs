using UnityEngine;

public class Shield_Item : Pickup_Item
{
    [SerializeField] private float shieldDuration;

    public override void Pickup(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Player_Effect effects = collider.GetComponent<Player_Effect>();
            Player_Health playerHealth = collider.GetComponent<Player_Health>();
            Entity entity = collider.GetComponent<Entity>();

            effects.CreateShield(collider.transform, shieldDuration);
            playerHealth?.ImmuneDamaged(shieldDuration);
            UI.Instance.IngameUI.IconBarUI.AddOrRefreshEffect("shield", sr.sprite, shieldDuration, entity);
        }
    }
}
