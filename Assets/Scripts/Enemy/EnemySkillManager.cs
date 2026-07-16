public class EnemySkillManager : EntitySkillManager
{
    private Enemy enemy;
    private Projectile_Arrow arrow;

    public override void Awake()
    {
        base.Awake();

        enemy = GetComponentInParent<Enemy>();
        arrow = GetComponentInChildren<Projectile_Arrow>();
    }

    public override void Start()
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
