using UnityEngine;

public class EntitySkillManager : MonoBehaviour
{
    public Entity entity { get; private set; }

    [SerializeField] protected SkillDataSO skillData;
    [SerializeField] protected Projectile_Base[] projectiles;

    protected virtual void Awake()
    {
        entity = GetComponentInParent<Entity>();
        Debug.Log("entity: " + entity.name);
        projectiles = GetComponentsInChildren<Projectile_Base>();
    }

    protected virtual void Start()
    {
        skillData = entity.characterData.skillData;
        GetSkill(skillData.skillType).SetupProjectile(skillData);
    }

    protected virtual void Update()
    {

    }

    public virtual Projectile_Base GetSkill(SkillType type)
    {
        return null;
    }
}
