public class AnimalState : EntityState
{
    protected Animal animal;

    public AnimalState(Animal animal, StateMachine<EntityState> stateMachine, Projectile_Base projectile, string animBoolName) : base(stateMachine, projectile, animBoolName)
    {
        this.animal = animal;
        anim = animal.anim;
        col = animal.col;
        rb = animal.rb;
    }
}
