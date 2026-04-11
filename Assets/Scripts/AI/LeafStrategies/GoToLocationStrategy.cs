using System;
using UnityEngine;
using ROGUE.Characters;

public class GoToLocationStrategy : ILeafStrategy
{
    NPC entity;
    Func<Vector3> func;
    Vector3 location;
    float remainingDistance = Mathf.Infinity;
    float speed;
    
    public GoToLocationStrategy(NPC entity, Func<Vector3> func, float speed = 0.5f)
    {
        this.entity = entity;
        this.func = func;
        this.speed = speed;
    }

    public void OnEnter()
    {
        location = func();
        remainingDistance = Vector3.Distance(entity.transform.position, location);
        entity.navAgent.SetDestination(location);
        entity.move.SetSpeed(speed);
    }

    public void OnLeave()
    {
        remainingDistance = Mathf.Infinity;
        entity.move.SetSpeed(0f);
    }

    public Node.Status OnProcess(float deltaTime)
    {
        remainingDistance = Vector3.Distance(entity.transform.position, location);
        if(remainingDistance <= entity.navAgent.stoppingDistance || remainingDistance <= 0.5f) return Node.Status.SUCCESS;
        else return Node.Status.RUNNING;
    }
}