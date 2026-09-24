using System;
using System.Collections;
using UnityEngine;

public class Player_Health : Entity_Health
{
    public Action OnPlayerDied;

    private Player player;
    private bool isDamaged;
    private Coroutine immuneDamagedCo;
    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<Player>();
    }

    protected override void Start()
    {
        base.Start();
        UI.Instance.UpdateHealthBarUI(currentHealth, maxHealth);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        OnHealthChanged += UI.Instance.UpdateHealthBarUI;
        OnPlayerDied += UI.Instance.UpdateTotalSummaryUI;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        OnHealthChanged -= UI.Instance.UpdateHealthBarUI;
        OnPlayerDied -= UI.Instance.UpdateTotalSummaryUI;
    }

    public override bool TakeDamage(int damage, bool isCrit)
    {
        if (isDamaged) return false;

        if (base.TakeDamage(damage, isCrit))
        {
            player.effect.HurtEffect();
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
            return true;
        }

        return false;
    }

    protected override void Dead()
    {
        base.Dead();
        player.health.OnPlayerDied?.Invoke();
    }

    public void ImmuneDamaged(float duration)
    {
        if (immuneDamagedCo != null)
            StopCoroutine(immuneDamagedCo);

        immuneDamagedCo = StartCoroutine(ImmuneDamagedCo(duration));
    }

    private IEnumerator ImmuneDamagedCo(float duration)
    {
        IsDamaged(true);
        yield return new WaitForSeconds(duration);
        IsDamaged(false);
    }

    public void IsDamaged(bool damaged) => isDamaged = damaged;
}
