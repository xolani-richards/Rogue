using UnityEngine;

namespace ROGUE.Characters
{
    public class CharacterEvents: MonoBehaviour
    {
        [SerializeField] AnimationClip hitClip;
        [SerializeField] AnimationClip deathClip;

        Character character;

        void Awake()
        {
        character = GetComponent<Character>();   
        }
        public void OnTakeHit ()
        {
            if(character.health.health <= 0) return;
            character.animationSystem.PlayOneShot(hitClip);
        }

        public void OnDead ()
        {
            character.canMove = false;
            character.canRotate = false;
            character.isExecuting = true;
            character.animationSystem.PlayOneShot(deathClip);
            Invoke("onDead", deathClip.length - 0.3f);
        }

        public void OnDetectNME ()
        {}

        public void OnLoseNME ()
        {}

        void onDead ()
        {
            character.animator.enabled = false;
            NPCController controller = character.GetComponent<NPCController>();
            if (controller != null) controller.enabled = false;
        }
    }
}