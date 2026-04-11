using System;
using ROGUE.Characters;

public class AttackStrategy : ILeafStrategy
{
    NPC npc;
    Attack attack;
    bool isAttacking = false;

    public AttackStrategy(NPC npc) 
    {
        this.npc = npc;
        this.attack = npc.GetComponent<Attack>();
    }

    public void OnEnter()
    {
        if(!attack.canAttack) return;
        attack.OnAttack();
    }

    public void OnLeave()
    {
        isAttacking = false;
    }

    public Node.Status OnProcess(float deltaTime)
    {
        if(!isAttacking) return Node.Status.FAILED;
        else return attack.isExecuting ? Node.Status.RUNNING: Node.Status.SUCCESS;
    }
}