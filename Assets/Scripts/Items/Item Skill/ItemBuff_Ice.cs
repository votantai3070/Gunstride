using UnityEngine;

[CreateAssetMenu(fileName = "Skill Buff - Chill", menuName = "Hybrid Casual/Skill Buff Data/Chill")]
public class ItemBuff_Ice : SkillBuffDataSO
{
    public override void ApplyEffect(GameObject playerObject)
    {
        Player player = playerObject.GetComponent<Player>();
        if (player == null)
            return;

        player.skillManager.GetSkillByType(skillType).CombineUpgrade(this);
    }
}