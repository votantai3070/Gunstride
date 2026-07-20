using UnityEngine;

public class SkillItem : MonoBehaviour
{
    protected Utils utils = new();
    private Player player;

    [SerializeField] protected SkillDataSO skillData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        player = collision.GetComponent<Player>();

        Projectile_Base skill = player.skillManager.GetSkillByType(skillData.skillType);

        if (skill.upgradeType == SkillUpgradeType.None)
            skill.SetupProjectile(skillData);
        else
            skill.CombineUpgrade(skillData);

        // ObjectPool.instance.Despawn(gameObject);
    }
}