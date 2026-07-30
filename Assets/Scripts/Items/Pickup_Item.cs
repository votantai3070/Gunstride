using UnityEngine;

public class Pickup_Item : MonoBehaviour, IPickupable
{
    public virtual void Pickup(Collider2D collider)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Pickup(collision);
    }
}
