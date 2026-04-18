using System;
using System.Collections.Generic;
using UnityEngine;
using ROGUE.Characters;
using MEC;

namespace ROGUE.Abilities
{
    [CreateAssetMenu(fileName = "", menuName = "ROGUE/Ability")]
    public class Ability: ScriptableObject 
    {
        [field: Header("Details")]
        [field: SerializeField] public string displayName { get; protected set; } 
        [field: SerializeField] public Sprite icon { get; protected set; }
        [field: SerializeField, TextArea] public string description { get; protected set; }
        

        [field: Header("FX")]
        [field: SerializeField] public GameObject preCastFX { get; protected set; } = null;
        [field: SerializeField] public AudioClip castSfx { get; protected set; }
        [field: SerializeField] public Vector3 castVfxOffset { get; protected set; } = new Vector3(0, 1f, 0);
        [field: SerializeField] public GameObject castVfx { get; protected set; }
        [field: SerializeField] public GameObject runningVfx { get; protected set; }

        [field: Header("Animation")]
        [field: SerializeField] public AnimationClip animation { get; protected set; }
        [field: SerializeField] public float animationSpeed { get; protected set; } = 1f;
        [field: SerializeField] public float castDelay { get; protected set; }
        public Action<GameObject> OnCompleted;

        [Header("Targeting")]
        public TargetingStrategy targetingStrategy;
        
        [Header("Effects")]
        public EffectInstance[] effects;

        // CURRENTLY THIS IS ACTUALLY THE TRIGGER
        public void Target(TargetingManager targetingManager) {
        if (targetingStrategy != null) targetingStrategy.OnStart(this, targetingManager);
        // currentUser = targetingManager;
        }

        public void Execute(GameObject caster, IEffectTarget target)
        {
            // play animation
            // play vfx
            // on anim event
            if(animation != null)
            {
                Character character = caster.GetComponent<Character>();
                character.animationSystem.PlayOneShot(animation,animationSpeed);
            }
            if (preCastFX != null) Instantiate(preCastFX, caster.transform.position + castVfxOffset, Quaternion.identity);
            
            Timing.RunCoroutine(Run(caster, target));
        }


        public void ApplyEffects(GameObject caster, IEffectTarget target)
        {
            foreach (var effectItem in effects) effectItem.Apply(caster, target);
        }

        void HandleVFX(IEffectTarget target) 
        {
            // Vector3 offset = new Vector3(0,2f,0);
            var targetMb = target as MonoBehaviour;
            if (targetMb == null) return;

            if (castVfx != null) {
                Instantiate(castVfx, targetMb.transform.position + castVfxOffset, Quaternion.identity);
            }

            if (runningVfx != null) {
                var runningVfxInstance = Instantiate(runningVfx, targetMb.transform);
                Destroy(runningVfxInstance, 3f);
            }
        }

        IEnumerator<float> Run(GameObject caster, IEffectTarget target)
        {
            yield return Timing.WaitForSeconds(castDelay / animationSpeed);
            HandleVFX(target);
            ApplyEffects(caster, target);
            if(animation != null) yield return Timing.WaitForSeconds((animation.length - castDelay) / animationSpeed);
            OnCompleted?.Invoke(caster);
        }
    }

    [Serializable]
    public class AbilityItem
    {
        public Ability ability;
        public float cooldown;
        public float timer;
        public bool isExecuting = false;
        public Action onUpdate;
        public Action onReady;
        
        public void OnStartTargeting(TargetingManager targetingManager) {
            ability.Target(targetingManager);
            isExecuting = true;
            onUpdate?.Invoke();
        }
        public void SetTimer ()
        {
            timer = cooldown;
            onUpdate?.Invoke();
        }

        public void onTick(float deltaTime)
        {
            if(timer == 0) return;
            timer = Mathf.Clamp(timer - deltaTime, 0, cooldown);
            onUpdate?.Invoke();
            if(timer == 0f) onReady?.Invoke();
        }
    }
}