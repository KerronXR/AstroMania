using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 1.25f;
    public Vector3 offset;
    public bool isCameraShaking;
    public bool isCameraShakingHard;

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        if (desiredPosition.y - transform.position.y > 0.8f) desiredPosition.y = transform.position.y + 0.8f;
        if (isCameraShaking && !isCameraShakingHard)
        {
            desiredPosition.y = Mathf.Lerp(transform.position.y, ((desiredPosition.y - 0.5f) + (Random.value * 2)), 1000f * Time.deltaTime);
        }
        else if (isCameraShakingHard)
        {
            desiredPosition.y = Mathf.Lerp(transform.position.y, ((desiredPosition.y - 2f) + (Random.value * 4)), 4000f * Time.deltaTime);
        }
        else 
        {
            desiredPosition.y = Mathf.Lerp(transform.position.y, desiredPosition.y, 100f * Time.deltaTime);
        }
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothPosition;
    }
}