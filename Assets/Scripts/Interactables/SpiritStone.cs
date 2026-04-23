using UnityEngine;

public class SpiritStone: MonoBehaviour, IVisitor
{
    public int points = 10;
    IActor actor;
        
    public void OnHeroVisit()
    {
        Logger.Debug(this, "Hero stepped on stone.");
        PointManager.instance.AddPoints(points);
        Destroy(gameObject, 0.2f);
    }

    public void OnNPCVisit()
    {
        Logger.Debug(this, "NPC stepped on stone.");
    }

    void OnTriggerEnter(Collider other)
    {
        actor = other.GetComponent<IActor>();
        if (actor != null) actor.OnAccept(this);
    }
}