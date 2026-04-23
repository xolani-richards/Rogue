using System;
using UnityEngine;
using Matso.Events;

namespace ROGUE.Characters
{
    [RequireComponent(typeof(PointManager))]
    [RequireComponent(typeof(Stamina))]
    public class Hero : Character, IActor, IHealable
    {
        public static Hero instance;
        [HideInInspector] public CharacterMove move;
        [HideInInspector] public Attack attack;
        [HideInInspector] public Stamina stamina;
        Rigidbody rb;
        protected StateEngine stateEngine;

        protected override void Awake()
        {
            if(instance == null) instance = this;
            else Destroy(gameObject);

            base.Awake();
            animationSystem = new (animator);
            animationSystem.UpdateLocomotion(0f);
            stateEngine = GetComponent<StateEngine>();
            move = GetComponent<CharacterMove>();
            attack = GetComponent<Attack>();
            stamina = GetComponent<Stamina>();
            rb = GetComponent<Rigidbody>();
        }

        public override void OnAccept(IVisitor visitor) => visitor.OnHeroVisit();

        public void AddHealth(float amount)
        {
            if(amount <= 0) return;
            health.AddHealth(amount);
        }

        protected override void OnDied()
        {
            base.OnDied();
            EventBus.Publish(EventKey.HERO_DIED, null);
        }    
    }
}