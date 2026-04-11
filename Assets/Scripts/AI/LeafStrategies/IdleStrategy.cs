using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class IdleStrategy : ILeafStrategy
{
    float minDuration;
    float maxDuration;
    float duration;

    public IdleStrategy(float minDuration, float maxDuration)
    {
        this.minDuration = minDuration;
        this.maxDuration = maxDuration;
    }

    public void OnEnter()
    {
        duration = Random.Range(minDuration, maxDuration);
    }

    public void OnLeave()
    {
        duration = 0f;
    }

    public Node.Status OnProcess(float deltaTime)
    {
        duration -= deltaTime;
        return duration <= 0f ? Node.Status.SUCCESS: Node.Status.RUNNING;
    }
}