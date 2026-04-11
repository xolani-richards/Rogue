using System;
using UnityEngine;
using UnityEngine.AI;
using ROGUE.Characters;
using Random = UnityEngine.Random;

public class WanderStrategy : ILeafStrategy
{
    float range;
    Func<Vector3> func;
    Vector3 origin;
    NPC entity;
    Vector3 destination;

    public WanderStrategy(NPC entity, Func<Vector3> func, float range)
    {
        this.entity = entity;
        this.func = func;
        this.range = range;
    }

    public void OnEnter()
    {
        origin = func();
        destination = entity.transform.position;
        for(int i = 0; i < 10; i++)
        {
            Vector3 point = Random.insideUnitSphere * range + origin;
            if(NavMesh.SamplePosition(point, out NavMeshHit hit, 100f, 1))
            {
                destination = hit.position;
                return;
            }
        }
    }

    public void OnLeave()
    {
        origin = Vector3.positiveInfinity;
    }

    public Node.Status OnProcess(float deltaTime)
    {
        float distance = Vector3.Distance(entity.transform.position, destination);
        return distance < 0.25f ? Node.Status.SUCCESS: Node.Status.RUNNING;
    }
}