using System;
using UnityEngine;

namespace ROGUE.Characters
{
    [RequireComponent(typeof(PointManager))]
    [RequireComponent(typeof(HeroMove))]
    public class Hero : Character, IActor
    {
        public static Hero instance;
        [HideInInspector] public HeroMove move;
        [HideInInspector] public AbilityController abilityController;
        [HideInInspector] public Attack attack;
        Rigidbody rb;

        protected override void Awake()
        {
            if(instance == null) instance = this;
            else Destroy(gameObject);

            base.Awake();
            move = GetComponent<HeroMove>();
            abilityController = GetComponent<AbilityController>();
            attack = GetComponent<Attack>();
            rb = GetComponent<Rigidbody>();
        }

        public override void OnAccept(IVisitor visitor) => visitor.OnHeroVisit();
    }
}