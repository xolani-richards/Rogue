using ROGUE.Characters;
using UnityEngine;

[CreateAssetMenu(fileName ="PickUp_", menuName = "ROGUE/Pickups/Selection")]
public class SelectablePickup : InteractionSO
{
    [SerializeField] string title;
    [SerializeField] string content;

    public override bool OnInteract(IActor actor)
    {
        if(actor.gameObject != Hero.instance.gameObject) return false;
        UIManager uiManager = ServiceLocator.Get<UIManager>();
        uiManager?.ShowPopup(title, content, () => OnAccept(actor), () => {});
        return true;
    }

    void OnAccept(IActor actor)
    {
        Debug.Log("ACCEPTED");
    }
}