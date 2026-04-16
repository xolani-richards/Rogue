using UnityEngine;
using ROGUE.Characters;

public class Attack: MonoBehaviour
{
    [SerializeField] string contextKey;
    [SerializeField] float maxRange;
    [SerializeField] protected bool canQueueAttack;
    [SerializeField] protected bool attackQueued;
    [field: SerializeField] public bool canAttack { get; protected set; }
    [field: SerializeField] public bool isExecuting { get; protected set; }

    protected Character character;

    protected virtual void Start()
    {
        character = GetComponent<Character>();
        character.context.SetData(contextKey, maxRange);
    }

    public void OnAttack ()
    {
        if(canAttack) StartAttack();    
        else if(canQueueAttack) attackQueued = true;
    }

    protected virtual void StartAttack ()
    {}
}