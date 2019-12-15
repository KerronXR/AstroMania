using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadScreenMove : MonoBehaviour
{
    private float length, startPos;
    public GameObject cam;
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cam.transform.position.x > (startPos + length + length / 2))
        {
            startPos += (3 * length);
            transform.position = new Vector3(startPos, transform.position.y, transform.position.z);

        }
        else if (cam.transform.position.x < (startPos - length - length / 2))
        {
            startPos -= (3 * length);
            transform.position = new Vector3(startPos, transform.position.y, transform.position.z);
        }

    }
}