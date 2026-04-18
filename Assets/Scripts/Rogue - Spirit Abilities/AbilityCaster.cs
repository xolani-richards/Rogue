using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ROGUE.Abilities
{
    public class AbilityCaster : MonoBehaviour
    {
        [field: SerializeField] public int abilitySlotLimit { get; private set; }= 3;
        [SerializeField] List<AbilityItem> abilities = new();
        
        [SerializeField] AbilityItem currentAbility;
        [SerializeField] bool isExecuting = false;
        [SerializeField] int selectedIndex = 0;
        HeroController heroController;
        TargetingManager targetingManager;

        public Action onAbilitiesUpdated;
        public Action onSelectionUpdated;

        void Awake()
        {
            targetingManager = GetComponent<TargetingManager>();
            heroController = GetComponent<HeroController>();
            heroController.onNext += SelectNext;
            heroController.onPrevious += SelectPrevious;
            SetSelectedAbility(selectedIndex);
        }

        public AbilityItem[] GetAbilityItems() => abilities.ToArray();
        public AbilityItem GetCurrentAbility() => currentAbility;

        public bool AddAbilityItem(AbilityItem item)
        {
            if(isExecuting || abilities.Count >= abilitySlotLimit) return false;
            abilities.Add(item);
            onAbilitiesUpdated?.Invoke();
            if(abilities.Count == 1) SetSelectedAbility(selectedIndex);
            return true;
        }

        public bool AddAbilityItemToSlot(AbilityItem item, int slot)
        {
            if(isExecuting || slot >= abilitySlotLimit) return false;
            if(slot > abilities.Count) abilities.Add(item);
            else abilities[slot] = item;
            onAbilitiesUpdated?.Invoke();
            return true;
        }

        public void RemoveAbiltiyFromSlot(int slot)
        {
            if(isExecuting || slot >= abilities.Count) return;
            abilities.RemoveAt(slot);
            onAbilitiesUpdated?.Invoke();
        }

        void SelectNext () => Select(1);
        void SelectPrevious () => Select(-1);

        void Select(int index)
        {
            if(abilities.Count <= 1 || isExecuting) return;
            selectedIndex += index;
            if(selectedIndex < 0) selectedIndex = abilities.Count - 1;
            else if(selectedIndex >= abilities.Count) selectedIndex = 0;
            SetSelectedAbility(selectedIndex);
        }

        void SetSelectedAbility(int index)
        {
            if(index >= abilities.Count) return;
            currentAbility = abilities[index];
            onSelectionUpdated?.Invoke();
        }

        void Update()
        {
            foreach (AbilityItem ability in abilities)
            {
                if(ability.timer > 0) ability.onTick(Time.deltaTime);
            }
            if (isExecuting) return;
            if (heroController.cast) Cast(abilities[selectedIndex]);
        }

        bool CanCast(AbilityItem item, out string msg)
        {
            if(isExecuting)  { msg = "ability in use"; return false; }
            if(item.timer > 0) { msg = "on cooldown"; return false; }
            
            msg = string.Empty;
            return true;
        }

        void Cast(AbilityItem item)
        {
            if(!CanCast(item, out string msg)) {
                Debug.Log($"CAST FAILED: {msg}");
                return;
            }

            Debug.Log($"Casting: {item.ability.displayName}");
            isExecuting = true;
            currentAbility = item;
            currentAbility.ability.OnCompleted += OnCompleted;
            currentAbility.OnStartTargeting(targetingManager);

            // PlaySound effects
            if (currentAbility.ability.castSfx)
            {
                AudioSource.PlayClipAtPoint(currentAbility.ability.castSfx, transform.position);
            }
        }

        private void OnCompleted(GameObject @object)
        {
            if(@object != targetingManager.gameObject) return;
            isExecuting = false;
            if(currentAbility == null) return;
            currentAbility.isExecuting = false;
            currentAbility.SetTimer();
        }
    }
}