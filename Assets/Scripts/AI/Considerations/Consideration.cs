using UnityEngine;
using ROGUE.Characters;

public abstract class Consideration: ScriptableObject
{
    public abstract float Evaluate (NPC entity);
}