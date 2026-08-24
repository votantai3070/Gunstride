using UnityEngine;

public class Heart_Item : Pickup_Item
{
    [SerializeField] private float healAmount;

    public override void Pickup(Player player)
    {
        if (player.TryGetComponent<IHealable>(out var healable))
        {
            healable.IncreaseHealth(healAmount);
            ObjectPool.Instance.Despawn(gameObject);
        }
    }
}
