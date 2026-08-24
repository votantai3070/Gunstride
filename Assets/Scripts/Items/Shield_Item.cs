using UnityEngine;

public class Shield_Item : Pickup_Item
{
    [SerializeField] private float shieldDuration;

    public override void Pickup(Player player)
    {
        if (player.CompareTag("Player"))
        {
            Player_Effect effects = player.GetComponent<Player_Effect>();
            Player_Health playerHealth = player.GetComponent<Player_Health>();

            effects.CreateShield(player.transform, shieldDuration);
            playerHealth?.ImmuneDamaged(shieldDuration);
            UI.Instance.IngameUI.IconBarUI.AddOrRefreshEffect("shield", sr.sprite, shieldDuration, player);
        }
    }
}
