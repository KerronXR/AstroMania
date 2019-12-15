using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenMove : MonoBehaviour
{
    private float height, startPos;
    public GameObject cam;
    void Start()
    {
        startPos = transform.position.y;
        height = GetComponent<SpriteRenderer>().bounds.size.y;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cam.transform.position.y > (startPos + height + height / 2))
        {
            startPos += (3 * height);
            transform.position = new Vector3(transform.position.x, startPos, transform.position.z);

        }
        else if (cam.transform.position.y < (startPos - height - height / 2))
        {
            startPos -= (3 * height);
            transform.position = new Vector3(transform.position.x, startPos, transform.position.z);
        }

    }
}
