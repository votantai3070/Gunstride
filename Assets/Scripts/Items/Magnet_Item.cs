using UnityEngine;

public class Magnet_Item : Pickup_Item
{
    [SerializeField] private float duration;

    public override void Pickup(Player player)
    {
        player.UseMagnet(duration);
    }
}
