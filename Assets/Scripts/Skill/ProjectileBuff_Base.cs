using System.Linq;
using TMPro;
using UnityEngine;

public class ProjectileBuff_Base : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private SkillBuff_ListDataSO datas;
    [SerializeField] protected SkillBuffDataSO skillBuffData;
    [SerializeField] private TextMeshPro buffText;

    private Player player;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        player = FindFirstObjectByType<Player>();

        animator.runtimeAnimatorController = skillBuffData.skillAnim;
    }

    private void Start()
    {
        RefreshText();
    }

    private void OnDisable()
    {
    }

    public void RefreshText()
    {
        if (buffText == null || player == null || skillBuffData == null)
            return;

        var skill = player.skillManager.GetSkillByType(skillBuffData.skillType);

        if (skill == null)
            return;

        if (!skill.HasUpgrade(skillBuffData.upgradeType))
        {
            buffText.text = skillBuffData.upgradeType.ToString();
            return;
        }

        buffText.text = GetUpgradeText();
    }

    private string GetUpgradeText()
    {
        return skillBuffData switch
        {
            ItemBuff_Additional additional =>
                $"+{additional.amount}",

            ItemBuff_Bounce bounce =>
                $"+{bounce.bounceCount} {bounce.upgradeType}",

            ItemBuff_Pierce pierce =>
                $"+{pierce.pierceCount} {pierce.upgradeType}",

            _ => skillBuffData.upgradeType.ToString()
        };
    }

    private SkillBuffDataSO[] FindAllListDataBySkillType(SkillType skillType)
    {
        if (datas == null || datas.skillList == null)
            return null;

        return datas.skillList
            .Where(item => item != null && item.skillType == skillType)
            .ToArray();
    }

    protected virtual void ApplyEffect(GameObject player)
    {
        skillBuffData.ApplyEffect(player);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ApplyEffect(collision.gameObject);
            ItemManager.Instance.RefreshAllBuffTextsInvoke();
            ObjectPool.Instance.Despawn(gameObject);
        }
    }
}
