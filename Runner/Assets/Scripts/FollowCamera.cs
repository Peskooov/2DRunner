using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Camera newCamera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offsetPosition;
    [SerializeField] private Vector3 offsetRotation;
    [SerializeField] private float smoothSpeed;

    private void LateUpdate()
    {
        Quaternion targetRotation = target.rotation;

        Vector3 setPosition = target.position + offsetPosition;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, setPosition, smoothSpeed);

        transform.position = smoothedPosition;

        Vector3 setRotation = target.position + offsetRotation;

        transform.LookAt(setRotation);
    }
}
