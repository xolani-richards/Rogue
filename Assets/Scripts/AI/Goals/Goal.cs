using UnityEngine;
using ROGUE.Characters;

public class Goal: ScriptableObject
{
    [field: SerializeField] public string displayName { get; protected set; } 
        [field: SerializeField] public float score { get; protected set; }
        [SerializeField] CompoundConsideration consideration;
        [SerializeField] protected Node behaviour;
        protected NPC entity;
        public virtual void Bind (NPC entity)
        {
            this.entity = entity;
        }

        public float Evaluate()
        {
            score = consideration.Evaluate(entity);
            return score;
        }

        public void OnStart () => behaviour.OnEnter();
        public void OnExit () => behaviour.OnExit();
        public Node.Status Process(float deltaTime) => behaviour.Process(deltaTime);
}