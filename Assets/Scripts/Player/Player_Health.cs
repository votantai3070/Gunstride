public class Player_Health : Entity_Health
{
    private Player player;
    private bool isDamaged;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<Player>();
    }

    protected override void Start()
    {
        base.Start();

        UI.Instance.UpdateHealthBarUI(currentHealth, maxHealth);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        OnHealthChanged += UI.Instance.UpdateHealthBarUI;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        OnHealthChanged -= UI.Instance.UpdateHealthBarUI;
    }

    public override bool TakeDamage(int damage)
    {
        //if (isDamaged) return false;

        if (base.TakeDamage(damage))
        {
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
            player.entityEffects.HurtEffect();
            return true;
        }

        return false;
    }

    public void IsDamaged(bool damaged) => isDamaged = damaged;
}
