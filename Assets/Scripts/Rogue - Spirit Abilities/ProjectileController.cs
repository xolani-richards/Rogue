using UnityEngine;
namespace ROGUE.Abilities
{
    public class ProjectileController : MonoBehaviour {
        Ability ability;
        float speed;

        public void Initialize(Ability ability, float speed) {
            this.ability = ability;
            this.speed = speed;
            Destroy(gameObject, 5f);
        }
        
        void Update() => transform.Translate(Vector3.forward * (speed * Time.deltaTime));

        void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Player")) return;

            if (other.gameObject.TryGetComponent<IEffectTarget>(out var target)) {
                ability.Execute(gameObject, target);
                Destroy(gameObject);
            }
        }
    }
}