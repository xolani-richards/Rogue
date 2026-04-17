using System;
using UnityEngine;

namespace ROGUE.Characters
{
    public class Health: MonoBehaviour
    {
        [field: SerializeField] public float health { get; protected set; }
        [field: SerializeField] public float maxHealth { get; protected set; }
        public Action healthUpdated;
        public Action died;
        public float normalized () => health / maxHealth;
        Character character;

        void Start()
        {
            character = GetComponent<Character>();
            if(character == null) return;
            maxHealth = character.stats.MaxHealth;
            health = maxHealth;
        }

        public void AddHealth(float value)
        {
            if (value <= 0) return;
            float newHealth = Mathf.Clamp(health + value, 0, maxHealth);
            if(newHealth == health) return;
            health = newHealth;
            healthUpdated?.Invoke();
        }

        public void RemoveHealth(float value)
        {
            if (value <= 0 || health <= 0) return;
            float newHealth = Mathf.Clamp(health - value, 0, health);
            if(newHealth == health) return;
            health = newHealth;
            healthUpdated?.Invoke();
            if(health <= 0) died?.Invoke();
        }
    }
}