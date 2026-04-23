using System;
using UnityEngine;

[Serializable]
public class AttackData
{
    [SerializeField] string displayName;
    public AnimationClip clip;
    public float playbackSpeed;
    public float blendDelay;
    public float baseStaminaCost;
}