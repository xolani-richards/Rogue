using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class Ability {
    public AudioClip castSfx;
    public GameObject castVfx;
    public GameObject runningVfx;
    
    [Header("Effects")]
    // [SerializeReference] public List<IEffectFactory<IDamageable>> effects = new();
    [SerializeReference] public List<IEffect<IDamageable>> effects = new();
    
    [Header("Targeting")]
    [SerializeReference] TargetingStrategy targetingStrategy;

    public void Target(TargetingManager targetingManager) {
        if (targetingStrategy != null) {
            targetingStrategy.OnStart(this, targetingManager);
        }
    }

    public void Execute(IDamageable target) {
        HandleVFX(target);
        
        foreach (var effect in effects) {
            var runtimeEffect = effect;//.Create();
            target.ApplyEffect(runtimeEffect);
        }
    }

    void HandleVFX(IDamageable target) {
        Vector3 offset = new Vector3(0,2f,0);
        var targetMb = target as MonoBehaviour;
        if (targetMb == null) return;

        if (castVfx != null) {
            Object.Instantiate(castVfx, targetMb.transform.position + offset, Quaternion.identity);
        }

        if (runningVfx != null) {
            var runningVfxInstance = Object.Instantiate(runningVfx, targetMb.transform);
            Object.Destroy(runningVfxInstance, 3f);
        }
    }
}