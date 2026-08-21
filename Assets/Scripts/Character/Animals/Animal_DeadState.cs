public class Animal_DeadState : AnimalState
{
    public Animal_DeadState(Animal animal, StateMachine<EntityState> stateMachine, Projectile_Base projectile, string animBoolName) : base(animal, stateMachine, projectile, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
