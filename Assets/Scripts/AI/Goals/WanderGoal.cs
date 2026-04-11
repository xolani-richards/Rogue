using UnityEngine;
using UnityEngine.AI;
using ROGUE.Characters;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "GOAL_Wander", menuName = "ROGUE/AI/Goals/Wander")]
public class WanderGoal: Goal
{
    [SerializeField] float range;
    [SerializeField] int areaMask = 1;

    public override void Bind(NPC entity)
    {
        base.Bind(entity);
        Sequencer sequencer = new Sequencer("Wander");
        sequencer.AddChild(new Leaf("Go To Destination", new GoToLocationStrategy(entity, () => GetTargetDestination(entity))));
        sequencer.AddChild(new Leaf("Rest", new IdleStrategy(3f, 5f)));
        behaviour = sequencer;
    }

    Vector3 GetTargetDestination(NPC entity)
    {
        Vector3 pos = entity.transform.position;
        for (int i = 0; i < 10; i++)
        {
            Vector3 sample = pos + Random.insideUnitSphere * range;
            sample.y = pos.y;
            if(NavMesh.SamplePosition(sample, out NavMeshHit hit, range, areaMask))
            {
                return hit.position;
            }
        }
        return pos;
    }
}