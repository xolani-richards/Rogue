using UnityEngine;
using MEC;
using ROGUE.Characters;
using System.Collections.Generic;

public class SpiritStoneDropper: MonoBehaviour
{
    [SerializeField] SpiritStone prefab;
    [SerializeField] float delay = 3f;
    [SerializeField] float dropRadius = 1f;
    [SerializeField] int value;

    Character character;

    void Awake ()
    {
        character = GetComponent<Character>();
        character.onDied += CreateItem;
    }

    void OnDestroy()
    {
        if (character != null) character.onDied -= CreateItem;
    }

    void CreateItem () => Timing.RunCoroutine(Spawn());

    IEnumerator<float> Spawn ()
    {
        yield return Timing.WaitForSeconds(delay);
        Vector3 pos = transform.position + (Random.insideUnitSphere * dropRadius);
        pos.y = transform.position.y;

        SpiritStone instance = Instantiate (prefab, pos, Quaternion.identity);
        instance.points = value;
    }
}