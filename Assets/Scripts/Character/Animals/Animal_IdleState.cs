public class Animal_IdleState : AnimalState
{
    public Animal_IdleState(Animal animal, StateMachine<EntityState> stateMachine, Projectile_Base projectile, string animBoolName) : base(animal, stateMachine, projectile, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = animal.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(animal.walkState);
    }
}
