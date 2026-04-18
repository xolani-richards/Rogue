using System;
using ROGUE.Characters;
using UnityEngine;

public enum Statename
{
    None = 0,
    Idle,
    Moving,
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
    protected StateEngine engine;

    public State(StateEngine engine)
    {
        this.engine = engine;
    }
    public abstract void OnEnter ();
    public abstract void OnExit ();
    public abstract void OnCheckState ();
    public abstract void OnUpdate(float deltaTime);
}

[Serializable]
public class StateEngine: MonoBehaviour
{
    [SerializeField] StateFactory stateFactory;
    [field: SerializeField] public Statename statename;
    protected State currentState;
    protected BaseStateFactory factory;
    protected Character character;
    public Context context => character.context;
    

    public void Awake()
    {
        character = GetComponent<Character>();
        // character.context.onUpdate += CheckState;
        factory = stateFactory.Init(this);
        ChangeState(Statename.Idle);
    }

    void OnDestroy()
    {
        // character.context.onUpdate -= CheckState;
    }

    void Update()
    {
        if (currentState == null) return;
        currentState.OnUpdate(Time.deltaTime);
    }

    public void ChangeState (Statename newState)
    {
        State nextState = factory.GetState (newState);
        if(nextState == null || nextState == currentState) return;
        if(currentState != null) currentState.OnExit ();
        currentState = nextState;
        currentState.OnEnter ();
        statename = currentState.statename;
    }

    public void CheckState ()  => currentState?.OnCheckState();
    
}