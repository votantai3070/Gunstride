public class EnemySkillManager : EntitySkillManager
{
    private EnemyRange enemy;
    private Projectile_Arrow arrow;

    protected override void Awake()
    {
        base.Awake();

        enemy = GetComponentInParent<EnemyRange>();
        arrow = GetComponentInChildren<Projectile_Arrow>();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override Projectile_Base GetSkill(SkillType type)
    {
        return type switch
        {
            SkillType.Arrow => arrow,
            _ => null
        };
    }
}
