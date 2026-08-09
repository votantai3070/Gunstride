using UnityEngine;

[CreateAssetMenu(fileName = "Skill Buff - Chill", menuName = "Hybrid Casual/Skill Buff Data/Chill")]
public class ItemBuff_Slow : SkillBuffDataSO
{
    public int slowStacksPerHit = 1;
    public float slowDuration = 2f;
    public float slowPercentPerStack = 0.2f;
    public int freezeThreshold = 3;
    public float freezeDuration = 1.5f;

    public override void ApplyEffect(GameObject playerObject)
    {

    }
}