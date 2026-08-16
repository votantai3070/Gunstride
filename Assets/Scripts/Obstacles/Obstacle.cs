using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Utils utils = new Utils();
    protected Player player;

    [SerializeField] protected ObstacleDataSO obstacleData;

    private bool isTriggerAttack;
    private float lastTimeAttack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTriggerAttack) return;

        lastTimeAttack = Time.time;

        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<Player>();

            utils.CanAttack(lastTimeAttack, obstacleData.duration);

            IDamageable damageable = collision.GetComponent<IDamageable>();
            bool canHit = damageable.TakeDamage(obstacleData.damage);

            if (canHit)
            {
                Player_Effect effects = player.GetComponent<Player_Effect>();
                effects.HurtEffect();
            }
        }
    }

    public void IsTriggerAttack(bool canAttack) => isTriggerAttack = canAttack;
}
