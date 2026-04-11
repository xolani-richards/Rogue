using System;
using System.Collections.Generic;
using UnityEngine;
using ROGUE.Characters;

[Serializable]
public class CompoundConsideration
{
    public enum Mode { ADD, MAX, MIN, AVG, MUL }
    [SerializeField] List<Consideration> considerations = new ();
    [SerializeField] Mode mode;
    [SerializeField] bool allowZeroResult;

    public float Evaluate(NPC entity)
    {
        float value = 0;
        switch (mode)
        {
            case Mode.ADD:
                value = OnAdd(entity);
                break;
            
            case Mode.MAX:
                value = OnMax(entity);
                break;

           case Mode.MIN:
                value = OnMin(entity);
                break;

           case Mode.AVG:
                value = OnAvg(entity);
                break;

            case Mode.MUL:
                value = OnMultiply(entity);
                break; 
        }
        
        return value;
    }

    float OnAdd(NPC entity)
    {
        float value = 0;
        foreach (Consideration consideration in considerations)
        {
            float score = consideration.Evaluate(entity);
            if(score <= 0 && !allowZeroResult) return 0;
            value += score;
        }
        return value;
    }

    float OnMax(NPC entity)
    {
        float value = 0;
        foreach (Consideration consideration in considerations)
        {
            float score = consideration.Evaluate(entity);
            if(score <= 0 && !allowZeroResult) return 0;
            if(score > value) value = score;
        }
        return value;
    }

    float OnMin(NPC entity)
    {
        float value = 1;
        foreach (Consideration consideration in considerations)
        {
            float score = consideration.Evaluate(entity);
            if(score <= 0 && !allowZeroResult) return 0;
            if(score < value) value = score;
        }
        return value;
    }

    float OnAvg(NPC entity)
    {
        float value = OnAdd(entity);
        if(value <= 0 && !allowZeroResult) return 0;
        return value / considerations.Count;
    }

    float OnMultiply (NPC entity)
    {
        float value = 1;
        foreach (Consideration consideration in considerations)
        {
            float score = consideration.Evaluate(entity);
            if(score <= 0 && !allowZeroResult) return 0;
            value *= score;
        }
        return value;
    }

}