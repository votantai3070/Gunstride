public class Enemy : Entity
{
    public EnemySkillManager skillManager { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        skillManager = GetComponentInChildren<EnemySkillManager>();
    }

    public void FlippedLeft()
    {
        if (flipped) return;

        flipped = true;
        utils.FlipLeft(transform);
    }
}
