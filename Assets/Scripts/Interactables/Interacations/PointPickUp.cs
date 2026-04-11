using UnityEngine;

[CreateAssetMenu(fileName ="PickUp_", menuName = "ROGUE/Pickups/Points")]
public class PointPickUp : InteractionSO
{
    [SerializeField] int value;
    public override bool OnInteract(IActor actor)
    {
        if (value <= 0) return false;
        PointManager pointManager = actor.gameObject.GetComponent<PointManager>();
        if (pointManager == null) return false;
        pointManager.AddPoints(value); 
        return true;
    }
}