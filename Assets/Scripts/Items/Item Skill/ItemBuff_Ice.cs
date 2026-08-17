using UnityEngine;

[CreateAssetMenu(fileName = "Skill Buff - Chill", menuName = "Hybrid Casual/Skill Buff Data/Skill Buff - Chill")]
public class ItemBuff_Ice : SkillBuffDataSO
{
    public override void ApplyEffect(GameObject playerObject)
    {
        Player player = playerObject.GetComponent<Player>();
        if (player == null)
            return;

        if (!playerObject.TryGetComponent<Player_Combat>(out var combat)) return;

        combat.SetElement(GetElementType());
    }
}