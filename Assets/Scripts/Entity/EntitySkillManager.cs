using UnityEngine;

public class EntitySkillManager : MonoBehaviour
{
    public Entity entity { get; private set; }

    [SerializeField] protected SkillDataSO skillData;
    [SerializeField] protected Projectile_Base[] projectiles;

    public virtual void Awake()
    {
        entity = GetComponentInParent<Entity>();
        projectiles = GetComponentsInChildren<Projectile_Base>();
    }

    public virtual void Start()
    {
        GetSkill(skillData.skillType).SetupProjectile(skillData);
    }

    public virtual void Update()
    {

    }

    public virtual Projectile_Base GetSkill(SkillType type)
    {
        return null;
    }
}
