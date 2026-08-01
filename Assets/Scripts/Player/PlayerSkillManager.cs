using Managers;

public class PlayerSkillManager : EntitySkillManager
{
    public Player player { get; private set; }
    private Projectile_WindSlash windSlash { get; set; }
    private Projectile_Arrow arrow { get; set; }

    protected override void Awake()
    {
        base.Awake();

        player = GetComponentInParent<Player>();
        windSlash = GetComponentInChildren<Projectile_WindSlash>();
        arrow = GetComponentInChildren<Projectile_Arrow>();
    }


    protected override void Update()
    {
        foreach (var projectile in projectiles)
        {
            if (projectile.CanUseSkill() && GameManager.Instance.IsGameStarted())
                projectile.UseSkill();
        }
    }

    public override Projectile_Base GetSkillByType(SkillType type)
    {
        return type switch
        {
            SkillType.WindSlash => windSlash,
            SkillType.Arrow => arrow,
            _ => null
        };
    }

}
