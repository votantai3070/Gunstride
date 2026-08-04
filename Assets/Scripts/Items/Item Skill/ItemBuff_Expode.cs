using UnityEngine;

[CreateAssetMenu(fileName = "Skill Buff - Explode", menuName = "Hybrid Casual/Skill Buff Data/Skill Buff - Explode")]
public class ItemBuff_Expode : SkillBuffDataSO
{
    [Header("Explode")]
    public float explodeRadius = 1.5f;
    public int explodeDamage = 1;
    public LayerMask explodeTargetMask;

    public override void ApplyEffect(GameObject playerObject)
    {
        Player player = playerObject.GetComponent<Player>();
        if (player == null)
            return;

        player.skillManager.GetSkillByType(skillType).CombineUpgrade(this);
    }
}
