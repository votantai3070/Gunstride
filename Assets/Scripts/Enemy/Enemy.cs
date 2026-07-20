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
        idleTime = characterData.skillData.upgradeData.cooldown;
    }

    protected virtual void OnEnable()
    {
        flipped = false;
        FlippedLeft();
    }

    protected virtual void OnDisable()
    {
        flipped = false;
    }

    public void FlippedLeft()
    {
        if (flipped) return;

        flipped = true;
        utils.FlipLeft(transform);
    }
}
