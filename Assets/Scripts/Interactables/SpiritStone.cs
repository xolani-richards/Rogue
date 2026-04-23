using UnityEngine;

public class SpiritStone: MonoBehaviour, IVisitor
{
    public int points = 10;
    IActor actor;
        
    public void OnHeroVisit()
    {
        Debug.Log("HERO");
        PointManager.instance.AddPoints(points);
        Destroy(gameObject, 0.2f);
    }

    public void OnNPCVisit()
    {
        Debug.Log("NPC?");
    }

    void OnTriggerEnter(Collider other)
    {
        actor = other.GetComponent<IActor>();
        if (actor != null) actor.OnAccept(this);
    }
}