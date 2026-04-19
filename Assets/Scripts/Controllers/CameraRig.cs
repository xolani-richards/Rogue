using UnityEngine;
using ROGUE.Characters;
using Unity.Cinemachine;

public class CameraRig : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform mount;
    [SerializeField] CinemachineCamera cam;
    [SerializeField] float mountAngle = 10f;
    [SerializeField] float cameraDistance = 10f;
    [SerializeField] float cameraSmoothSpeed;
    [SerializeField] float fieldOfView;
    Vector3 cameraVelocity;

    // void OnValidate()
    // {
    //     mount.rotation = Quaternion.Euler(mountAngle, 0f, 0f);
    //     cam.transform.localPosition = new Vector3(0f, 0f, 0f - Mathf.Abs(cameraDistance));
    //     cam.Lens.FieldOfView = fieldOfView;
    // }

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
