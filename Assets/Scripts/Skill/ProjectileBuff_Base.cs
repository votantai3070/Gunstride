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

        if (skillBuffData is ItemBuff_Additional additional)
            buffText.text = $"+{additional.amount}";
        else
            buffText.text = $"{skillBuffData.upgradeType}";

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
            // Optionally, you can destroy the buff object after applying the buff
        }
    }
}
