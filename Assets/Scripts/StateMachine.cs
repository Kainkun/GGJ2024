public abstract class State
{
    public virtual void OnEnter() {}
    public virtual void OnExit() {}
}

public class StateMachine<T> where T : State
{
    public T CurrentState { get; private set; }
    
    public void TransitionTo(T state)
    {
        CurrentState?.OnExit();
        CurrentState = state;
        CurrentState?.OnEnter();
    }
}
