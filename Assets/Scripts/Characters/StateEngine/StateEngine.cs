using System;
using UnityEngine;

public enum Statename
{
    None = 0,
    Idle,
    TakingDamage,
    Dying,
    Dead,
    Stunned,
    Attacking,
    Blocking,
    KnockBack,
}

public abstract class State
{
    [field: SerializeField] public Statename statename { get; protected set; }
    protected StateEngine stateEngine;

    public State(StateEngine engine)
    {
        this.stateEngine = engine;
    }
    public abstract void OnEnter ();
    public abstract void OnExit ();
    public abstract void OnCheckState ();
}

[Serializable]
public class StateEngine
{
    // [SerializeField] StateFactory stateFactory;
    [field: SerializeField] public Statename statename;
    protected State currentState;
    protected BaseStateFactory factory;
    public Context context;

    public StateEngine(Context context, StateFactory stateFactory)
    {
        factory = stateFactory.Init(this);
        ChangeState(Statename.Idle);
    }

    public void ChangeState (Statename newState)
    {
        State nextState = factory.GetState (newState);
        if(nextState == null) return;
        if(currentState != null) currentState.OnExit ();
        currentState = nextState;
        currentState.OnEnter ();
        statename = currentState.statename;
    }

    public void CheckState () => currentState?.OnCheckState();
}