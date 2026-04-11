using UnityEngine;
using ROGUE.Characters;

[CreateAssetMenu(fileName = "GOAL_Chase_Hero", menuName = "ROGUE/AI/Goals/Chase Hero")]
public class ChaseHero: Goal
{
    [SerializeField] float range;

    public override void Bind(NPC npc)
    {
        base.Bind(npc);
        Sequencer sequencer = new Sequencer("Chase");
        // sequencer.AddChild(new Leaf("In Range", new ConditionStrategy(() => )));
        sequencer.AddChild(new Leaf("Go To Destination", new GoToTargetStrategy(npc, () => Hero.instance?.transform, npc.attackDistance, 1f)));
        behaviour = sequencer;
    }
}