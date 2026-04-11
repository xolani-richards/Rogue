using System;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using ROGUE.Characters;

[CreateAssetMenu(fileName = "ABT_", menuName = "ROGUE/Abililties/Ability")]
public class AbilityData : ScriptableObject
{
    [field: SerializeField] public string displayName { get; protected set; }
    [field: SerializeField, Range(0.1f, 4f)] public float castTime { get; protected set; } = 2f;
    [field: SerializeField, Range(0.1f, 4f)] public float duration { get; protected set; } = 1f;
    [field: SerializeField] public float cooldown { get; protected set; } = 0.1f;
    [field: SerializeField] public HitBox vfx; 
    

    [field: Header("Animation")]
    [field: SerializeField] public AnimationClip clip { get; protected set; }
    [field: SerializeField] public float playbackSpeed { get; protected set; } = 1f;

    [field: Header("Effects")]
    [field: SerializeField] public List<AbilityEffectItem> effects { get; protected set; } = new();
    
    [field: Header("Movement")]
    [field: SerializeField] public bool canMove { get; protected set; } = false;
    [field: SerializeField] public bool canRotate { get; protected set; } = false;

    void OnEnable()
    {
        if (string.IsNullOrEmpty(displayName)) displayName = name;
    }
}

[Serializable]
public struct AbilityEffectItem
{
    [SerializeField] string name;
    [SerializeField] float value;
    [SerializeField] AbilityEffect effect;

    public void Execute(GameObject caster, GameObject target) => effect.Execute(caster, target, value);

    void OnEnable()
    {
        if (name == null) name = effect.name;
    }
}

public abstract class AbilityEffect : ScriptableObject
{
    public abstract void Execute(GameObject caster, GameObject target, float value);

    protected void DisableMovement(Character character, float duration = 0.5f)
    {
        Timing.RunCoroutine(onDisableMovement(character, duration));
    }

    IEnumerator<float> onDisableMovement(Character character, float duration)
    {
        character.canMove = false;
        character.canRotate = false;
        yield return Timing.WaitForSeconds(duration);
        character.canMove = true;
        character.canRotate = true;
    }
}

public abstract class TimedAbilityEffect: AbilityEffect
{
    [field: SerializeField] public float duration { get; protected set; }
}
