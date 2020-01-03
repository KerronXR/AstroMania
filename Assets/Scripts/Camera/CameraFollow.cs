using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 1.25f;
    public Vector3 offset;
    public bool isCameraShaking;

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        if (desiredPosition.y - transform.position.y > 0.8f) desiredPosition.y = transform.position.y + 0.8f;
        if (isCameraShaking)
        {
            desiredPosition.y = Mathf.Lerp(transform.position.y, ((desiredPosition.y - 0.5f) + (Random.value * 2)), 1000f * Time.deltaTime);
        }
        else
        {
            desiredPosition.y = Mathf.Lerp(transform.position.y, desiredPosition.y, 10f * Time.deltaTime);
        }
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothPosition;
    }
}