using UnityEngine;

public class FaceCamera : MonoBehaviour {
    [SerializeField] Transform cameraRig;

    void Start ()
    {
        Camera camera = Camera.main;
        cameraRig = camera.transform;
    }

    private void LateUpdate() 
    {
        transform.right = cameraRig.right;
    }
}