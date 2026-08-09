using System.Collections;
using UnityEngine;

public class Enemy : Entity
{
    public EnemySkillManager skillManager { get; private set; }


    protected override void Awake()
    {
        base.Awake();

        skillManager = GetComponentInChildren<EnemySkillManager>();
    }

    protected override void Start()
    {
        base.Start();
        idleTime = characterData.skillData.cooldown;
    }


    protected override void OnEnable()
    {
        base.OnEnable();

        flipped = false;
        FlippedLeft();
    }

    protected virtual void OnDisable()
    {
        flipped = false;
    }

    protected override IEnumerator SlowDownCo(float duration)
    {
        float originalSpeed = speed;
        float originalAnim = anim.speed;

        speed = speed * moveSpeedMultiplier;
        anim.speed = anim.speed * moveSpeedMultiplier;

        yield return new WaitForSeconds(duration);

        speed = originalSpeed;
        anim.speed = originalAnim;

        slowDownCo = null;
    }

    public void FlippedLeft()
    {
        if (flipped) return;

        flipped = true;
        utils.FlipLeft(transform);
    }
}
