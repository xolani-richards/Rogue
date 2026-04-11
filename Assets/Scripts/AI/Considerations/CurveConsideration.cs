using System;
using UnityEngine;
using ROGUE.Characters;

[CreateAssetMenu(fileName = "", menuName = "ROGUE/AI/Considerations/curve")]
public class CurveConsideration : Consideration
{
    [SerializeField] AnimationCurve curve;
    [SerializeField] string key;
    public override float Evaluate(NPC entity) => curve.Evaluate(entity.context.GetData(key));
}