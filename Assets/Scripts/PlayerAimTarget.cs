using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimTarget : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;

    void Update()
    {
        SetTarget();
    }

    void SetTarget()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out RaycastHit hit, 100f, layerMask))
        {
            transform.position = hit.point;
        }
    }
}