using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPiece : MonoBehaviour
{
    private float flySpeedX, flySpeedY;
    public Rigidbody2D rb;
    private float rotateTo;
    private float creationTime;
    void Start()
    {
        creationTime = Time.timeSinceLevelLoad;
        rotateTo = (float)(Random.value * 360);
        flySpeedX = (Random.value * 2 - 1) * 10;
        if (flySpeedX > 0) flySpeedX += 10;
        else flySpeedX -= 10;
        flySpeedY = Random.value * 30;
        transform.Rotate(0, 0, rotateTo);
        rb.AddTorque(Random.value * 550);
        rb.velocity = new Vector2(flySpeedX, flySpeedY);
    }

    void Update()
    {
        if ((Time.timeSinceLevelLoad - creationTime) > 5)
        {
            Destroy(gameObject);
        }
    }
}
