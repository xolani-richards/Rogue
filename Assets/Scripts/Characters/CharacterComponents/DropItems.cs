using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using MEC;

[Serializable]
public struct DropItem
{
    [field: SerializeField, Range(0,1)] public float dropChance;
    [field: SerializeField] public GameObject item;
}

public class DropItems: MonoBehaviour
{
    [SerializeField] List<DropItem> dropItems = new ();
    [SerializeField] float delay = 3f;
    [SerializeField] float dropRadius = 1f;

    public void CreateDropItems () => Timing.RunCoroutine(Spawn());

    IEnumerator<float> Spawn ()
    {
        yield return Timing.WaitForSeconds(delay);
        foreach (var dropItem in dropItems)
        {
            if(Random.Range(0f,1f) > dropItem.dropChance) continue;
            Vector3 pos = transform.position + (Random.insideUnitSphere * dropRadius);
            pos.y = transform.position.y;

            Instantiate(dropItem.item, pos, Quaternion.identity); 
        }
    }
}