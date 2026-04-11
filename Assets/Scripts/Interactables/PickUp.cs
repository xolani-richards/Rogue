using UnityEngine;

public class PickUp : MonoBehaviour, IInteractable, IVisitor
{
    [SerializeField] InteractionSO interactionSO;
    [SerializeField] bool canNPCInteract;
    [SerializeField] bool destroyAfterUse;
    [SerializeField] float destroyDelay = 0.25f;
    [SerializeField] float interactionRange = 1f;

    IActor actor;

    void Awake()
    {
        SphereCollider collider = GetComponent<SphereCollider>();
        if (collider != null) collider.radius = interactionRange;
        interactionSO = Instantiate(interactionSO);
    }

    void OnTriggerEnter(Collider other)
    {
        IActor actor = other.GetComponentInParent<IActor>();
        if (actor != null) OnVisit(actor);
    }

    public void OnHeroVisit() => OnInteract();

    public void OnNPCVisit()
    {
        Debug.Log("NPC: interact");
        if(canNPCInteract) OnInteract();
    }

    public void OnVisit(IActor actor) 
     {
        this.actor = actor;
        actor.OnAccept(this);
    }

    public void OnInteract() 
    {
        bool result = interactionSO.OnInteract(actor);
        if(result && destroyAfterUse) Destroy(gameObject, destroyDelay);
    }
}