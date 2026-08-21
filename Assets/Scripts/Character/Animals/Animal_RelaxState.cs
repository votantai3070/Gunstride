public class Animal_RelaxState : AnimalState
{
    public Animal_RelaxState(Animal animal, StateMachine<EntityState> stateMachine, Projectile_Base projectile, string animBoolName) : base(animal, stateMachine, projectile, animBoolName)
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
