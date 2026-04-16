using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ROGUE.Abilities
{
    [Serializable]
    public class AbilityItem
    {
        public Ability ability;
        public float cooldown;
        public void OnStartTargeting(TargetingManager targetingManager) => ability.Target(targetingManager);
    }
    public class AbilityCaster : MonoBehaviour
    {
        [SerializeField] List<AbilityItem> abilities = new();
        [SerializeField] TargetingManager targetingManager;
        [SerializeField] AbilityItem currentAbility;


        void Awake()
        {
            targetingManager = GetComponent<TargetingManager>();
        }

        void Update()
        {
            if (Keyboard.current.leftCtrlKey.wasPressedThisFrame) Cast(abilities[0]);
        }

        void Cast(AbilityItem item)
        {
            Debug.Log($"Casting: {item.ability.displayName}");
            currentAbility = item;
            currentAbility.OnStartTargeting(targetingManager);

            // PlaySound effects
            if (currentAbility.ability.castSfx)
            {
                AudioSource.PlayClipAtPoint(currentAbility.ability.castSfx, transform.position);
            }
        }
        

    }
}