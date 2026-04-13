using UnityEngine;

[RequireComponent(typeof(TargetingManager))]
public class PlayerAbilityCaster : MonoBehaviour {
    public Ability[] hotbar;
    public TargetingManager targetingManager;

    void Update() {
        for (int i = 0; i < hotbar.Length; i++) {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) {
                Cast(hotbar[i]);
            }
        }
    }

    void Cast(Ability ability) {
        ability.Target(targetingManager);

        if (ability.castSfx) {
            AudioSource.PlayClipAtPoint(ability.castSfx, transform.position);
        }
    }
}