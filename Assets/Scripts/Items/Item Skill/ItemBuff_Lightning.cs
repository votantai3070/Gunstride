using UnityEngine;

[CreateAssetMenu(fileName = "Skill Buff - Shock", menuName = "Hybrid Casual/Skill Buff Data/Skill Buff - Shock")]
public class ItemBuff_Lightning : SkillBuffDataSO
{
    public override void ApplyEffect(GameObject playerObject)
    {
        if (!playerObject.TryGetComponent<Player>(out var player))
            return;

        if (!playerObject.TryGetComponent<Player_Combat>(out var combat)) return;

        combat.SetElement(GetElementType());
        player.skillManager.GetSkillByType(skillType).SetElementType(GetElementType());
    }
}
