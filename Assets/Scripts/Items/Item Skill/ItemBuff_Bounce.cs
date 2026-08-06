using UnityEngine;

[CreateAssetMenu(fileName = "Skill Buff - Bounce", menuName = "Hybrid Casual/Skill Buff Data/Skill Buff - Bounce")]
public class ItemBuff_Bounce : SkillBuffDataSO
{
    public int bounceCount = 1;
    public float bounceRadius = 3f;
    public LayerMask targetMask;

    public override void ApplyEffect(GameObject playerObject)
    {
        Player player = playerObject.GetComponent<Player>();
        if (player == null)
            return;

        player.skillManager.GetSkillByType(skillType).CombineUpgrade(this);
    }
}
