using UnityEngine;


[CreateAssetMenu(fileName = "Skill Buff - Pierce", menuName = "Hybrid Casual/Skill Buff Data/Skill Buff - Pierce")]
public class ItemBuff_Pierce : SkillBuffDataSO
{
    [Header("Pierce Buff Settings")]
    public int pierceCount = 1;

    public override void ApplyEffect(GameObject playerObject)
    {
        Player player = playerObject.GetComponent<Player>();
        if (player == null)
            return;

        Projectile_Base projectile = player.skillManager.GetSkillByType(skillType);

        if (!projectile.HasUpgrade(upgradeType))
            player.skillManager.GetBuffSkill(this);
    }
}
