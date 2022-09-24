public abstract class State<T>
{
    protected StateMachine<T> stateMachine;
    protected T stateMachineClass;

    public void SetStateMachineWithClass(StateMachine<T> _stateMachine, T _stateMachineClass)
    {
        this.stateMachine = _stateMachine;
        this.stateMachineClass = _stateMachineClass;

        OnAwake();
    }

    public abstract void OnAwake();
    public abstract void OnStart();
    public abstract void OnUpdate(float deltaTime);
    public abstract void OnEnd();
    public abstract void OnHitEvent();
}
