using UnityEngine;
using UnityEngine.InputSystem;

namespace ROGUE.Abilities
{
    [CreateAssetMenu(fileName = "PointerProximityTargeting", menuName = "ROGUE/Targeting/PointerProximity", order = 0)]
    public class PointerProximityTargeting : TargetingStrategy
    {
        [SerializeField] LayerMask layerMask;
        [SerializeField] float radius;

        public override void OnStart(Ability ability, TargetingManager targetingManager)
        {
            IEffectTarget target = GetTarget();
            // Debug.Log($"Target: {target?.gameObject.name}");
            // if (target != null) 
            ability.Execute(targetingManager.gameObject, target);

        }

        IEffectTarget GetTarget ()
        {
            IEffectTarget target = null;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hitpoint, 100f, layerMask))
            {
                Collider[] hits = Physics.OverlapSphere(hitpoint.point, radius);
                foreach(Collider hit in hits)
                {
                    target = hit.GetComponentInParent<IEffectTarget>();
                    if(target != null) break;
                }
            }

            return target;
        }
    }
}