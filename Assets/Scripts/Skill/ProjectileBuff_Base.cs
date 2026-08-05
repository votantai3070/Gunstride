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

        animator.runtimeAnimatorController = skillBuffData.skillAnim;
    }

    private void Start()
    {
        if (skillBuffData is ItemBuff_Additional additional)
            buffText.text = $"+{additional.amount}";
        else
            buffText.text = $"{skillBuffData.upgradeType}";
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
            ObjectPool.Instance.Despawn(gameObject);
        }
    }
}
