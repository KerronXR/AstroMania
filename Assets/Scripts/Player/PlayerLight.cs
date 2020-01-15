using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    public bool isSliding;
    public bool isJumping;
    Vector2 desiredSlidePosition;
    Vector2 desiredJumpPosition;
    Vector2 desiredEndPosition;
    private void Start()
    {
        desiredJumpPosition = new Vector2(transform.localPosition.x + 0.5f, transform.localPosition.y + 0.9f);
        desiredSlidePosition = new Vector2(transform.localPosition.x - 1.9f, transform.localPosition.y - 1.9f);
        desiredEndPosition = transform.localPosition;
    }
    void Update()
    {
        if (isSliding)
        {
            Vector3 smoothPosition = Vector3.Lerp(transform.localPosition, desiredSlidePosition, 6f * Time.deltaTime);
            transform.localPosition = smoothPosition;
        }
        else if (isJumping)
        {
            Vector3 smoothPosition = Vector3.Lerp(transform.localPosition, desiredJumpPosition, 8f * Time.deltaTime);
            transform.localPosition = smoothPosition;
        }
        else
        {
            Vector3 smoothPosition = Vector3.Lerp(transform.localPosition, desiredEndPosition, 14f * Time.deltaTime);
            transform.localPosition = smoothPosition;
        }
       
    }
}
