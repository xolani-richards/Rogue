using UnityEngine;

namespace ROGUE.Abilities
{
    public class AbilityPickupTest : MonoBehaviour {
        [SerializeField] AbilityItem item;
        [SerializeField] bool destroyOnCollect;

        private void OnTriggerEnter(Collider other) {
            AbilityCaster caster = other.GetComponentInParent<AbilityCaster>();
            if(caster == null) return;
            if(caster.AddAbilityItem(item) && destroyOnCollect) Destroy(gameObject);
        }
    }
}