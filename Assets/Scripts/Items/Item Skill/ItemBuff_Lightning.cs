using UnityEngine;

[CreateAssetMenu(fileName = "Skill Buff - Shock", menuName = "Hybrid Casual/Skill Buff Data/Skill Buff - Shock")]
public class ItemBuff_Lightning : SkillBuffDataSO
{
    public override void ApplyEffect(GameObject playerObject)
    {
        if (!playerObject.TryGetComponent<Player>(out var player))
            return;

        player.skillManager.GetSkillByType(skillType).CombineUpgrade(this);
    }
}
