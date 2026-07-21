using UnityEngine;

public class Tornado : Obstacle
{
    private Rigidbody2D rb;

    [SerializeField] private float direction = -1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsGameStarted()) return;
        rb.linearVelocityX = obstacleData.speed * direction;
    }
}
