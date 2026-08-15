using UnityEngine;

[CreateAssetMenu(fileName = "Skill Buff - Burn", menuName = "Hybrid Casual/Skill Buff Data/Skill Buff - Burn")]
public class ItemBuff_Burn : SkillBuffDataSO
{
    public override void ApplyEffect(GameObject playerObject)
    {
        Player player = playerObject.GetComponent<Player>();
        if (player == null)
            return;

        player.skillManager.GetSkillByType(skillType).CombineUpgrade(this);
    }
}
