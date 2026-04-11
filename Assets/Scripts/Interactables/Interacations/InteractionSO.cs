using UnityEngine;

public abstract class InteractionSO : ScriptableObject
{
    public abstract bool OnInteract(IActor actor);
}