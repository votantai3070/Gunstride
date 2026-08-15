using UnityEngine;

public class EntitySkillManager : MonoBehaviour
{
    public Entity entity { get; private set; }

    [SerializeField] protected SkillDataSO skillData;
    [SerializeField] protected Projectile_Base[] projectiles;

    protected virtual void Awake()
    {
        entity = GetComponentInParent<Entity>();
        projectiles = GetComponentsInChildren<Projectile_Base>();
    }

    protected virtual void Start()
    {
        skillData = entity.characterData.skillData;
        GetSkillByType(skillData.skillType).SetupProjectile(skillData);
    }

    protected virtual void Update()
    {

    }

    public virtual Projectile_Base GetSkillByType(SkillType type)
    {
        return null;
    }

    public ElementType GetElementType(SkillType type)
    {
        Projectile_Base skill = GetSkillByType(type);

        if (skill.HasUpgrade(SkillUpgradeType.Chill))
            return ElementType.Ice;
        else if (skill.HasUpgrade(SkillUpgradeType.Burn))
            return ElementType.Fire;
        else if (skill.HasUpgrade(SkillUpgradeType.Shock))
            return ElementType.Lightning;

        return ElementType.None;
    }
}
