public class PlayerSkillManager : EntitySkillManager
{
    public Player player { get; private set; }
    private Projectile_WindSlash windSlash { get; set; }

    protected override void Awake()
    {
        base.Awake();

        player = GetComponentInParent<Player>();
        windSlash = GetComponentInChildren<Projectile_WindSlash>();
    }

    protected override void Start()
    {
        base.Start();
    }


    protected override void Update()
    {
        foreach (var projectile in projectiles)
        {
            if (projectile.CanUseSkill() && GameManager.Instance.IsGameStarted())
                projectile.UseSkill();
        }
    }

    public override Projectile_Base GetSkill(SkillType type)
    {
        return type switch
        {
            SkillType.WindSlash => windSlash,
            _ => null
        };
    }

}
