using UnityEngine;
using ROGUE.Characters;

public class CameraRig : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float cameraSmoothSpeed;
    Vector3 cameraVelocity;

    void Start()
    {
        target = Hero.instance?.transform;
        if(target == null) enabled = false;
    }

    void Update()
    {
        Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, target.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
        transform.position = targetCameraPosition;
    }

    
}
