using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TargetingManager))]
public class PlayerAbilityCaster : MonoBehaviour {
    public Ability[] hotbar;
    public TargetingManager targetingManager;

    void Update() 
    {
        if(Keyboard.current.eKey.wasPressedThisFrame) Cast(hotbar[0]);
        else if(Keyboard.current.rKey.wasPressedThisFrame) Cast(hotbar[1]);
        else if(Keyboard.current.tKey.wasPressedThisFrame) Cast(hotbar[2]);
    }

    void Cast(Ability ability) 
    {
        ability.Target(targetingManager);

        if (ability.castSfx) {
            AudioSource.PlayClipAtPoint(ability.castSfx, transform.position);
        }
    }
}