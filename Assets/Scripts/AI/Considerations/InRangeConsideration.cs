using UnityEngine;
using ROGUE.Characters;

[CreateAssetMenu(fileName = "", menuName = "ROGUE/AI/Considerations/In Range")]
public class InRangeConsideration : Consideration
{
    [SerializeField] AnimationCurve curve;
    [SerializeField] float maxDistance = 10f;
    [SerializeField] string targetTag;
    [SerializeField] string key;
    public override float Evaluate(NPC entity)
    {
        float maxRange = key == string.Empty ? maxDistance: entity.context.GetData(key);
        GameObject target = entity.sensor.GetClosestByTag(targetTag, maxDistance);

        Debug.Log(target);
        if (target == null) return 0f;
    
        float distance = Vector3.Distance(target.transform.position, entity.transform.position);
        
        return curve.Evaluate(distance/maxRange); 
    }
}