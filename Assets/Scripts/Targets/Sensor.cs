using UnityEngine;

public class Sensor: MonoBehaviour
{
    [SerializeField] LayerMask layerMask;

    public GameObject GetClosestByTag (string tag, float maxDistance)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, maxDistance, layerMask);
        
        GameObject closest = null;
        float range = maxDistance;

        foreach (Collider c in hits)
        {
            ITargetable targetable = c.GetComponentInParent<ITargetable>();
            if(targetable == null || targetable.tag != tag) continue;
            
            float distance = Vector3.Distance(transform.position, targetable.transform.position);
            if(distance > range) continue;
            closest = targetable.gameObject;
            range = distance;
        }

        return closest;
    }
}