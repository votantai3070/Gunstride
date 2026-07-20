public class Obstacle_AnimationEvents : Entity_AnimationEvents
{
    private Obstacle obstacle;

    private void Awake()
    {
        obstacle = GetComponentInParent<Obstacle>();
    }

    public void AttackTriggerStart()
    {
        obstacle.IsTriggerAttack(true);
    }

    public void AttackTrggierEnd()
    {
        obstacle.IsTriggerAttack(false);
    }
}
