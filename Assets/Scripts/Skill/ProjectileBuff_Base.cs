using TMPro;
using UnityEngine;

public class ProjectileBuff_Base : MonoBehaviour
{
    private Animator animator;
    [SerializeField] protected SkillBuffDataSO skillBuffData;
    [SerializeField] private TextMeshPro buffText;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        animator.runtimeAnimatorController = skillBuffData.skillAnim;
    }

    protected virtual void ApplyBuff(GameObject player)
    {
        ApplyBuffByUpgradeType(skillBuffData.upgradeData.upgradeType, player);
    }

    private void ApplyBuffByUpgradeType(SkillUpgradeType skillUpgradeType, GameObject player)
    {
        PlayerSkillManager playerSkillManager = player.GetComponentInChildren<PlayerSkillManager>();

        switch (skillUpgradeType)
        {
            case SkillUpgradeType.Single:
                buffText.text = $"+{skillBuffData.upgradeData.amount}";
                playerSkillManager?.GetSkillByType(skillBuffData.skillType)?.AdditionalProjectileCount(skillBuffData.upgradeData.amount);
                break;

            case SkillUpgradeType.Stun:
                // Apply stun upgrade logic
                break;

            case SkillUpgradeType.Bounce:
                // Apply bounce upgrade logic
                break;

            case SkillUpgradeType.Pierce:
                // Apply pierce upgrade logic
                break;

            case SkillUpgradeType.Explode:
                // Apply explode upgrade logic
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ApplyBuff(collision.gameObject);
            // Optionally, you can destroy the buff object after applying the buff
        }
    }
}
