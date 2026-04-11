using UnityEngine;

namespace ROGUE.Characters
{
    [CreateAssetMenu(fileName ="CHA_", menuName = "ROGUE/Configs/Character")]
    public class CharacterConfig: ScriptableObject
    {
        [Header("Animations")]
        [field: SerializeField] public AnimationClip idleAnim { get; protected set; }
        [field: SerializeField] public AnimationClip walkAnim { get; protected set; }
        [field: SerializeField] public AnimationClip runAnim { get; protected set; }

        [Header("Stats")]
        [field: SerializeField] public float maxHealth { get; protected set; } = 50f;
    }
}