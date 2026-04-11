using UnityEngine;

public abstract class Node
{
    public enum Status { RUNNING, SUCCESS, FAILED }

    [field: SerializeField] public string displayName { get; protected set; }
    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract Status Process(float deltaTime);
}