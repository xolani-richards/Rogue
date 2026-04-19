using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MEC;

public class NMESpawner: MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] float radius;
    [SerializeField] float frequency;
    CoroutineHandle handle;

    void Start()
    {
        handle = Timing.RunCoroutine(Process());
    }

    void OnDestroy()
    {
        Timing.KillCoroutines(handle);
    }

    public void Spawn ()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector2 point = Random.insideUnitCircle.normalized * radius;
            Vector3 pos = transform.position + new Vector3(point.x, 0f, point.y);
            if(NavMesh.SamplePosition(pos, out NavMeshHit hit, radius, 1))
            {
                Instantiate(prefab, pos, Quaternion.identity, transform);
                break;
            }
        }
    }

    private IEnumerator<float> Process ()
    {
        while(enabled)
        {
            yield return Timing.WaitForSeconds(frequency);
            Spawn();
        }
    }
}