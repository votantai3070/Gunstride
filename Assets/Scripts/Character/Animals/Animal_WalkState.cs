using UnityEngine;

public class Animal_WalkState : AnimalState
{
    public Animal_WalkState(Animal animal, StateMachine<EntityState> stateMachine, Projectile_Base projectile, string animBoolName)
        : base(animal, stateMachine, projectile, animBoolName)
    { }

    public override void Enter()
    {
        base.Enter();

        animal.SelectNextPatrolPoint();
        Debug.Log("Patrol Point: " + animal.GetPatrolPosition());
    }

    public override void Exit()
    {
        animal.StopMovement();

        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (animal.IsAtPatrolPoint())
        {
            stateMachine.ChangeState(animal.idleState);
            return;
        }

        animal.Patrol();
    }
}