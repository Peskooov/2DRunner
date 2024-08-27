using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Camera newCamera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed;

    private void LateUpdate()
    {
        Quaternion targetRotation = target.rotation;

        Vector3 desiredPosition = target.position + targetRotation * offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}
