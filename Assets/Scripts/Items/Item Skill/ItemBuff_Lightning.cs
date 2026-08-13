using UnityEngine;

[CreateAssetMenu(fileName = "Skill Buff - Lightning", menuName = "Hybrid Casual/Skill Buff Data/Skill Buff - Lightning")]
public class ItemBuff_Lightning : SkillBuffDataSO
{
    public override void ApplyEffect(GameObject playerObject)
    {
        if (!playerObject.TryGetComponent<Player>(out var player))
            return;

        player.skillManager.GetSkillByType(skillType).CombineUpgrade(this);
    }
}
