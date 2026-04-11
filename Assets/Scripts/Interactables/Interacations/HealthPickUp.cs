using UnityEngine;

[CreateAssetMenu(fileName ="PickUp_", menuName = "ROGUE/Pickups/Health")]
public class HealthPickUp : InteractionSO
{
    [SerializeField] int value;
    public override bool OnInteract(IActor actor)
    {
        actor.character.health.AddHealth(value);
        return true;
    }
}