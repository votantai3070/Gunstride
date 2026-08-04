using UnityEngine;

[CreateAssetMenu(fileName = "Skill Buff - Additional Projectile", menuName = "Hybrid Casual/Skill Buff Data/Skill Buff - Additional Projectile")]
public class ItemBuff_Additional : SkillBuffDataSO
{
    public int amount = 1;

    public override void ApplyEffect(GameObject playerObject)
    {
        Player player = playerObject.GetComponent<Player>();
        if (player == null)
            return;

        player.skillManager.GetSkillByType(skillType).AdditionalProjectile(amount);
    }
}
