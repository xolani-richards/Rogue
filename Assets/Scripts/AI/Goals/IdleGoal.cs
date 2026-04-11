using UnityEngine;
using ROGUE.Characters;

[CreateAssetMenu(fileName = "GOAL_Idle", menuName = "ROGUE/AI/Goals/Idle")]
public class IdleGoal: Goal
{
    [SerializeField] float min;
    [SerializeField] float max;

    public override void Bind(NPC entity)
    {
        base.Bind(entity);
        behaviour = new Leaf("Idle", new IdleStrategy(min, max));
    }
}
