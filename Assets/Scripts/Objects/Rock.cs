using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private float flySpeedX,flySpeedY;
    public Rigidbody2D rb;
    private float rotateTo;
    private float creationTime;
    private float previousYPosition;
    private bool isHit = false;
    void Start()
    {
        creationTime = Time.timeSinceLevelLoad;
        rotateTo = (float)(Random.value * 360);
        flySpeedX = Random.value * 20;
        flySpeedX = flySpeedX - 10;
        flySpeedY = Random.value * 10;
        transform.Rotate(0, 0, rotateTo);
        rb.velocity = new Vector2(flySpeedX, flySpeedY);
    }


    void Update()
    {
        if ((Time.timeSinceLevelLoad - creationTime) > 5)
        {
            Destroy(gameObject);
        }
        previousYPosition = rb.position.y;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (Mathf.Abs(Mathf.Abs(rb.position.y) - Mathf.Abs(previousYPosition)) > 0.1)
        {
            if (collision.collider.name == "Player" && !isHit)
            {
               // gameObject.GetComponent<Collider2D>().enabled = false;
                isHit = true;
                Player player = collision.collider.GetComponent<Player>();
                player.Hurt();
              //  Invoke("ReEnableCollider", 0.1f);
            }
        }

    }

 /*  void ReEnableCollider()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

 */
}
