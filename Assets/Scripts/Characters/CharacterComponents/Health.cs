using System;
using UnityEngine;

namespace ROGUE.Characters
{
    public class Health: MonoBehaviour
    {
        [field: SerializeField] public float hitpoints { get; protected set; }
        [field: SerializeField] public float maxHitpoints { get; protected set; }
        public Action healthUpdated;
        public Action died;
        public float normalized () => hitpoints / maxHitpoints;
        Character character;

        void Start()
        {
            character = GetComponent<Character>();
            if(character == null) return;
            maxHitpoints = character.stats.MaxHealth;
            hitpoints = maxHitpoints;
        }

        public void AddHealth(float value)
        {
            if (value <= 0) return;
            float newHealth = Mathf.Clamp(hitpoints + value, 0, maxHitpoints);
            if(newHealth == hitpoints) return;
            hitpoints = newHealth;
            healthUpdated?.Invoke();
        }

        public void RemoveHealth(float value)
        {
            if (value <= 0 || hitpoints <= 0) return;
            float newHealth = Mathf.Clamp(hitpoints - value, 0, hitpoints);
            if(newHealth == hitpoints) return;
            hitpoints = newHealth;
            healthUpdated?.Invoke();
            if(hitpoints <= 0) died?.Invoke();
        }
    }
}