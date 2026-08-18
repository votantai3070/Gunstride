using Managers;
using System.Collections;
using UnityEngine;

public class PlayerSkillManager : EntitySkillManager
{
    [Header("Additional Skill")]
    [SerializeField] private Sprite addIcon;

    [Header("Bounce Skill")]
    [SerializeField] private Sprite bounceIcon;

    [Header("Pierce Skill")]
    [SerializeField] private Sprite pierceIcon;

    [Header("Expolsion Skill")]
    [SerializeField] private Sprite explosionIcon;

    private Coroutine skillCo;

    public Player player { get; private set; }
    private Projectile_WindSlash windSlash { get; set; }
    private Projectile_Arrow arrow { get; set; }

    protected override void Awake()
    {
        base.Awake();

        player = GetComponentInParent<Player>();
        windSlash = GetComponentInChildren<Projectile_WindSlash>();
        arrow = GetComponentInChildren<Projectile_Arrow>();
    }


    protected override void Update()
    {
        foreach (var projectile in projectiles)
        {
            if (projectile.CanUseSkill() && GameManager.Instance.IsGameStarted())
                projectile.UseSkill();
        }
    }

    public void GetBuffSkill(SkillBuffDataSO skillBuffData)
    {
        if (skillBuffData is ItemBuff_Additional add)
        {
            UI.Instance.IngameUI.IconBarUI.AddOrRefreshEffect("Add", addIcon, skillBuffData.duration, player);
            AddtionalSkill(add, skillBuffData.duration);
        }

    }

    private void AddtionalSkill(ItemBuff_Additional add, float duration)
    {
        if (skillCo != null)
            StopCoroutine(skillCo);

        skillCo = StartCoroutine(AdditionalSkillCo(add, duration));
    }

    private IEnumerator AdditionalSkillCo(ItemBuff_Additional add, float duration)
    {
        GetSkillByType(add.skillType).AdditionalProjectile(add.amount);
        yield return new WaitForSeconds(duration);
        GetSkillByType(add.skillType).RemoveProjectile(add.amount);
    }

    public override Projectile_Base GetSkillByType(SkillType type)
    {
        return type switch
        {
            SkillType.WindSlash => windSlash,
            SkillType.Arrow => arrow,
            _ => null
        };
    }

}
