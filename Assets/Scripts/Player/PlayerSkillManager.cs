using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    public Player player { get; private set; }
    [SerializeField] private SkillDataSO[] skillDatas;
    [SerializeField] private Projectile_Base[] projectiles;

    private Projectile_WindSlash windSlash { get; set; }

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        projectiles = GetComponentsInChildren<Projectile_Base>();

        windSlash = GetComponentInChildren<Projectile_WindSlash>();
    }

    private void Start()
    {
        foreach (var skill in skillDatas)
        {
            GetSkill(skill.skillType).SetupProjectile(skill);
        }
    }


    private void Update()
    {
        foreach (var projectile in projectiles)
        {
            if (projectile.CanUseSkill() && GameManager.Instance.IsGameStarted())
                projectile.UseSkill();
        }
    }

    public Projectile_Base GetSkill(SkillType type)
    {
        return type switch
        {
            SkillType.WindSlash => windSlash,
            _ => null
        };
    }

}
