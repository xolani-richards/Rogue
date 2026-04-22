using UnityEngine;
using ROGUE.Characters;

[CreateAssetMenu(fileName = "consideration", menuName = "ROGUE/AI/Considerations/targetIsDead")]
public class TargetIsDeadConsideration : Consideration
{
    [SerializeField] float maxDistance = 10f;
    [SerializeField] string targetTag;

    public override float Evaluate(NPC entity)
    {
        GameObject target = entity.sensor.GetClosestByTag(targetTag, maxDistance);
        if (target == null) return 0f;
    
        Health health = target.GetComponent<Health>();
        return health?.hitpoints <= 0 ? 0f: 1f; 
    }
}