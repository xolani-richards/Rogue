using UnityEngine;
using ROGUE.Characters;

[CreateAssetMenu(fileName = "consideration", menuName = "ROGUE/AI/Considerations/constant")]
public class ConstantConsideration : Consideration
{
    [SerializeField] float value;
    public override float Evaluate(NPC entity) => value;
}