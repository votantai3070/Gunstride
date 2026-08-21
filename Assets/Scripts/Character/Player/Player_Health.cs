using System.Collections;
using UnityEngine;

public class Player_Health : Entity_Health
{
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
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        OnHealthChanged -= UI.Instance.UpdateHealthBarUI;
    }

    public override bool TakeDamage(int damage)
    {
        if (isDamaged) return false;

        if (base.TakeDamage(damage))
        {
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
            player.effect.HurtEffect();
            return true;
        }

        return false;
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
