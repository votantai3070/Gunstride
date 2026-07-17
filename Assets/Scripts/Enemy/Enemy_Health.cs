public class Enemy_Health : Entity_Health
{
    private Enemy enemy;
    private UI_HealthBar healthBar;

    public override void Awake()
    {
        base.Awake();
        enemy = GetComponent<Enemy>();
        healthBar = GetComponentInChildren<UI_HealthBar>();
    }

    public override void Start()
    {
        base.Start();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        OnHealthChanged += healthBar.UpdateHealthBarUI;
    }
}
