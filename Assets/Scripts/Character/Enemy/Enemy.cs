using System.Collections;
using UnityEngine;

public class Enemy : Entity
{
    public Enemy_Health health { get; private set; }
    public EnemySkillManager skillManager { get; private set; }

    private float originalSpeed;
    private float originalAnimSpeed;

    protected override void Awake()
    {
        base.Awake();

        health = GetComponent<Enemy_Health>();
        skillManager = GetComponentInChildren<EnemySkillManager>();
    }

    protected override void Start()
    {
        base.Start();

        originalSpeed = speed;
        originalAnimSpeed = anim.speed;

        idleTime = characterData.skillData.cooldown;
    }


    protected override IEnumerator SlowDownCo(float duration)
    {
        EntityStateHandler.SetElement(ElementType.Ice);
        speed = speed * moveSpeedMultiplier;
        anim.speed = anim.speed * moveSpeedMultiplier;

        yield return new WaitForSeconds(duration);

        StopSlowDown();
    }

    public override void StopSlowDown()
    {
        speed = originalSpeed;
        anim.speed = originalAnimSpeed;
        base.StopSlowDown();
    }
}
