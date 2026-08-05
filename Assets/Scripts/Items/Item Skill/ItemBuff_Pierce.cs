using UnityEngine;


[CreateAssetMenu(fileName = "Skill Buff - Pierce", menuName = "Hybrid Casual/Skill Buff Data/Skill Buff - Pierce")]
public class ItemBuff_Pierce : SkillBuffDataSO
{
    [Header("Pierce Buff Settings")]
    public int pierceCount = 3;

    public override void ApplyEffect(GameObject playerObject)
    {
        Player player = playerObject.GetComponent<Player>();
        if (player == null)
            return;

        player.skillManager.GetSkillByType(skillType).CombineUpgrade(this);
    }
}
