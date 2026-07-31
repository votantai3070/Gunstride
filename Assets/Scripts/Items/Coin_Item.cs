using UnityEngine;

public class Coin_Item : Pickup_Item
{
    [SerializeField] private int amount;

    public override void Pickup(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            GameManager.Instance.AddCoin(amount);
        }
    }
}
