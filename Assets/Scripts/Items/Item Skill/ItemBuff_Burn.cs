using UnityEngine;

[CreateAssetMenu(fileName = "Skill Buff - Burn", menuName = "Gunstrike Data/Skill Buff Data/Skill Buff - Burn")]
public class ItemBuff_Burn : SkillBuffDataSO
{
    public override void ApplyEffect(GameObject playerObject)
    {
        Player player = playerObject.GetComponent<Player>();
        if (player == null)
            return;

        if (!playerObject.TryGetComponent<Player_Combat>(out var combat)) return;

        combat.SetElement(GetElementType());
        player.skillManager.GetSkillByType(skillType).SetElementType(GetElementType());
    }
}
