using UnityEngine;

public class SkillItem : MonoBehaviour
{
    private Player player;

    [SerializeField] private SkillDataSO skillData;

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