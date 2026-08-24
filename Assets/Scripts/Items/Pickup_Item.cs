using UnityEngine;

public class Pickup_Item : MonoBehaviour, IPickupable
{
    protected SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    public virtual void Pickup(Player player)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Pickup(collision.GetComponent<Player>());
    }
}
