using UnityEngine;

public class Heart_Item : Pickup_Item
{
    [SerializeField] private float healAmount;

    public override void Pickup(Collider2D collider)
    {
        IHealable healable = collider.GetComponent<IHealable>();

        if (healable != null)
        {
            healable.IncreaseHealth(healAmount);
            ObjectPool.Instance.Despawn(gameObject);
        }
    }
}
