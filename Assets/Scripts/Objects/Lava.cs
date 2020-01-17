using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private float length, startPos;
    private Camera cam;
    public float Speed = 10f;
    void Start()
    {
        cam = Camera.main;
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * Time.deltaTime * Speed);
        startPos = transform.position.x;
        if (cam.transform.position.x > (startPos + length + length / 2))
        {
            startPos += (3 * length);
            transform.position = new Vector2(startPos, transform.position.y);

        }
        else if (cam.transform.position.x < (startPos - length - length / 2))
        {
            startPos -= (3 * length);
            transform.position = new Vector2(startPos, transform.position.y);
        }

    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.name == "Player")
        {
            Player player = collider.GetComponent<Player>();
            player.StandsOnLava();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.name == "Player")
        {
            Player player = collider.GetComponent<Player>();
            player.StopStandingOnLava();
        }
    }

}
