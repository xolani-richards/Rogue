using System;
using UnityEngine;

namespace ROGUE.Characters
{
    public class Stamina: MonoBehaviour
    {
        [field: SerializeField] public float staminaPoints { get; protected set; }
        [field: SerializeField] public float maxStaminaPoints { get; protected set; }
        public Action staminaUpdated;
        public Action staminaExpended;
        public float normalized () => staminaPoints / maxStaminaPoints;
        Character character;
        bool initalised;
        float cacheRecoveryRate;

        void Start()
        {
            if(initalised) return;
            character = GetComponent<Character>();
            if(character == null) return;
            maxStaminaPoints = character.stats.MaxStamina;
            staminaPoints = maxStaminaPoints;
            cacheRecoveryRate = character.stats.StaminaRecoveryRate;
        }

        void Update ()
        {
            if(staminaPoints == maxStaminaPoints) return;
            AddStamina(cacheRecoveryRate * Time.deltaTime);
        }

        public void Bind(Character character)
        {
            this.character = character;
            maxStaminaPoints = character.stats.MaxStamina;
            staminaPoints = maxStaminaPoints;
            initalised = true;
        }

        public bool HasStamina(float value) => staminaPoints >= value;

        public void AddStamina(float value)
        {
            if (value <= 0) return;
            float newStamina = Mathf.Clamp(staminaPoints + value, 0, maxStaminaPoints);
            if(newStamina == staminaPoints) return;
            staminaPoints = newStamina;
            staminaUpdated?.Invoke();
        }

        public void RemoveStamina(float value)
        {
            if (value <= 0 || staminaPoints <= 0) return;
            float newStamina = Mathf.Clamp(staminaPoints - value, 0, staminaPoints);
            if(newStamina == staminaPoints) return;
            staminaPoints = newStamina;
            staminaUpdated?.Invoke();
            if(staminaPoints <= 1f) staminaExpended?.Invoke();
        }
    }
}