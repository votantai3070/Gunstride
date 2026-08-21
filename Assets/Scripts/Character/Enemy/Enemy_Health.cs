public class Enemy_Health : Entity_Health
{
    private Enemy enemy;
    private UI_HealthBar healthBar;

    protected override void Awake()
    {
        base.Awake();
        enemy = GetComponent<Enemy>();
        healthBar = GetComponentInChildren<UI_HealthBar>();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        OnHealthChanged += healthBar.UpdateHealthBarUI;
        healthBar.UpdateHealthBarUI(currentHealth, maxHealth);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        OnHealthChanged -= healthBar.UpdateHealthBarUI;
    }
}
