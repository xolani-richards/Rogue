using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStateFactory
{
    protected StateEngine stateEngine;
    protected Dictionary<Statename, State> states;
    
    public BaseStateFactory (StateEngine engine)
    {
        stateEngine = engine;
        CreateStates();
    }

    protected abstract void CreateStates();

    public State GetState (Statename newState)
    {
        if(states == null || !states.ContainsKey(newState)) return null;
        return states[newState];
    }
}

public abstract class StateFactory: ScriptableObject
{
    public abstract BaseStateFactory Init(StateEngine engine);
}