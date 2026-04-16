using UnityEngine;

namespace ROGUE.Abilities
{
    [CreateAssetMenu(fileName = "ProjectileTargeting", menuName = "ROGUE/TargetingStrategy/Projectile", order = 0)]
    public class ProjectileTargeting : TargetingStrategy {
        public GameObject projectilePrefab;
        public float projectileSpeed = 10f;

        public override void OnStart(Ability ability, TargetingManager targetingManager) 
        {
            this.ability = ability;
            this.targetingManager = targetingManager;

            if (projectilePrefab != null) {
                Vector3 pos = targetingManager.transform.position + new Vector3(0,1,0); 
                // var flatForward = targetingManager.cam.transform.forward;
                var flatForward = targetingManager.transform.forward;
                flatForward.y = 0;
                flatForward.Normalize();
                
                var forwardRotation = Quaternion.LookRotation(flatForward);
                var projectile = Object.Instantiate(projectilePrefab, pos, forwardRotation);
                // projectile.GetComponent<ProjectileController>().Initialize(ability, projectileSpeed);
            }
        }
    }
}