using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TargetingManager))]
public class PlayerAbilityCaster : MonoBehaviour {
    public Ability[] hotbar;
    public TargetingManager targetingManager;

    void Update() {
        if(Keyboard.current.eKey.wasPressedThisFrame) Cast(hotbar[0]);
        else if(Keyboard.current.rKey.wasPressedThisFrame) Cast(hotbar[1]);
        else if(Keyboard.current.tKey.wasPressedThisFrame) Cast(hotbar[2]);
        // for (int i = 0; i < hotbar.Length; i++) {
        //     if (Input.GetKeyDown(KeyCode.Alpha1 + i)) {
        //         Cast(hotbar[i]);
        //     }
        // }
    }

    void Cast(Ability ability) {
        Debug.Log("Casting");
        ability.Target(targetingManager);

        if (ability.castSfx) {
            AudioSource.PlayClipAtPoint(ability.castSfx, transform.position);
        }
    }
}