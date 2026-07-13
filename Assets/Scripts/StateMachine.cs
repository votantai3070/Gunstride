public class StateMachine<T> where T : class, IState
{
    public T currentState { get; private set; }

    public void Initialize(T startState)
    {
        currentState = startState;
        (currentState as IState)?.Enter();
    }

    public void ChangeState(T newState)
    {
        (currentState as IState)?.Exit();
        currentState = newState;
        (currentState as IState)?.Enter();
    }
}

// Interface
public interface IState
{
    void Enter();
    void Update();
    void Exit();
}