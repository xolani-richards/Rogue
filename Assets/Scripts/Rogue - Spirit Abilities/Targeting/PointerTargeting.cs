using UnityEngine;
using UnityEngine.InputSystem;

namespace ROGUE.Abilities
{
    [CreateAssetMenu(fileName = "PointerTargeting", menuName = "ROGUE/Targeting/Pointer", order = 0)]
    public class PointerTargeting : TargetingStrategy
    {
        [SerializeField] LayerMask layerMask;

        public override void OnStart(Ability ability, TargetingManager targetingManager)
        {
            IEffectTarget target = GetTarget();
            Debug.Log($"Target: {target?.gameObject.name}");
            if (target != null) ability.Execute(targetingManager.gameObject, target);
        }

        IEffectTarget GetTarget ()
        {
            IEffectTarget target = null;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit, 100f, layerMask))
            {
                Debug.Log($"HIT: {hit.collider.name}");
                target = hit.collider.GetComponentInParent<IEffectTarget>();
            }

            return target;
        }
    }
}