public class Player_Health : Entity_Health
{
    private Player player;
    private bool isDamaged;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public override bool TakeDamage(int damage)
    {
        if (isDamaged) return false;

        if (base.TakeDamage(damage))
        {
            player.entityEffects.HurtEffect();
            return true;
        }

        return false;
    }

    public void IsDamaged(bool damaged) => isDamaged = damaged;
}
