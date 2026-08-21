public class Animal_HuntingState : AnimalState
{
    public Animal_HuntingState(Animal animal, StateMachine<EntityState> stateMachine, Projectile_Base projectile, string animBoolName) : base(animal, stateMachine, projectile, animBoolName)
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
