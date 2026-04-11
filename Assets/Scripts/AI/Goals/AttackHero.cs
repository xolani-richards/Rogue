using UnityEngine;
using ROGUE.Characters;

[CreateAssetMenu(fileName = "GOAL_Attack_Hero", menuName = "ROGUE/AI/Goals/Attack Hero")]
public class AttackHero: Goal
{
    public override void Bind(NPC npc)
    {
        base.Bind(npc);
        Sequencer sequencer = new Sequencer("Attack");
        sequencer.AddChild(new Leaf("Pause", new IdleStrategy(0f, 1f)));
        sequencer.AddChild(new Leaf("Attack", new AttackStrategy(npc)));
        sequencer.AddChild(new Leaf("Recover", new IdleStrategy(1f, 2f)));
        behaviour = sequencer;
    }
}