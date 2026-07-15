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
            if (CanUseSkill(projectile) && GameManager.Instance.IsGameStarted())
                projectile.UseSkill();
        }
    }

    private bool CanUseSkill(Projectile_Base projectile)
    {
        if (!projectile.CanUseSkill())
            return false;

        if (!player.DetectedTarget())
            return false;

        if (player.input.IsTracking())
            return false;

        return true;
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
