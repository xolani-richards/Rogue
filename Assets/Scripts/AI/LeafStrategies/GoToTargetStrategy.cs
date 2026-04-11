using System;
using UnityEngine;
using ROGUE.Characters;

public class GoToTargetStrategy : ILeafStrategy
{
    Func<Transform> func;
    Transform target;
    Vector3 destintaion;
    NPC entity;
    float speed;
    float targetDistance;
    float remainingDistance;
    float targetDelta;

    public GoToTargetStrategy(NPC entity, Func<Transform> func, float distance = 2f, float speed = 0.5f)
    {
        this.entity = entity;
        this.func = func;
        this.targetDistance = distance;
        this.speed = speed;
    }

    public void OnEnter()
    {
        target = func();
        destintaion = target.position;
        entity.navAgent.isStopped = false;
        entity.navAgent.SetDestination(destintaion);
        entity.move.SetSpeed(speed);
    }

    public void OnLeave()
    {
        entity.navAgent.isStopped = true;
        entity.move.SetSpeed(0f);
    }

    public Node.Status OnProcess(float deltaTime)
    {
        targetDelta = Vector3.Distance(target.transform.position, destintaion);
        if(targetDelta >= 0.5f)
        {
            destintaion = target.position;
            entity.navAgent.SetDestination(destintaion);
        }
        
        remainingDistance = Vector3.Distance(entity.transform.position, destintaion);
        if(remainingDistance <= targetDistance) return Node.Status.SUCCESS;
        return Node.Status.RUNNING;
    }
}