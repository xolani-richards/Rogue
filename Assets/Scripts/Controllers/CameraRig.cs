using UnityEngine;
using ROGUE.Characters;

public class CameraRig : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform mount;
    [SerializeField] Camera camera;
    [SerializeField] float mountAngle = 10f;
    [SerializeField] float cameraDistance = 10f;
    [SerializeField] float cameraSmoothSpeed;
    [SerializeField] float fieldOfView;
    Vector3 cameraVelocity;

    void OnValidate()
    {
        mount.rotation = Quaternion.Euler(mountAngle, 0f, 0f);
        camera.transform.localPosition = new Vector3(0f, 0f, 0f - Mathf.Abs(cameraDistance));
        camera.fieldOfView = fieldOfView;
    }

    void Start()
    {
        // target = Hero.instance?.transform;
        if(target == null) enabled = false;
    }

    void Update()
    {
        Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, target.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
        transform.position = targetCameraPosition;
    }

    
}
