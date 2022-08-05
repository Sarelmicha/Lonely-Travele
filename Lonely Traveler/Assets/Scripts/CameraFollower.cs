using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    private Vector3 initalOffset;
    private Vector3 cameraPosition;

    private void Start()
    {
        initalOffset = transform.position - targetObject.position;
    }

    private void FixedUpdate()
    {
        cameraPosition = targetObject.position + initalOffset;
        transform.position = cameraPosition;
    }
}