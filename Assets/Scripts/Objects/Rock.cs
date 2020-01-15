using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private float flySpeedX, flySpeedY;
    public Rigidbody2D rb;
    private float rotateTo;
    private float creationTime;
    private bool isHit = false;
    private float rockScale;
    public SpriteRenderer image;
    public GameObject Explosion;
    public GameObject[] pieces;
    void Start()
    {
        rockScale = 1 - Random.value / 2;
        transform.localScale = new Vector2(rockScale, rockScale);
        rb.mass *= rockScale;
        creationTime = Time.timeSinceLevelLoad;
        rotateTo = (float)(Random.value * 360);
        flySpeedX = Random.value * 20;
        flySpeedX = flySpeedX - 10;
        flySpeedY = Random.value * 10;
        transform.Rotate(0, 0, rotateTo);
        rb.AddTorque(Random.value * 1500);
        rb.velocity = new Vector2(flySpeedX, flySpeedY);
    }

    void Update()
    {
        if ((Time.timeSinceLevelLoad - creationTime) > 15)
        {
            Destroy(gameObject);
        }
    }
    private void OnMouseDown()
    {
        Vector2 whereToPutExplosion = new Vector2(transform.position.x, transform.position.y);
        GameObject ExplosionQ = Instantiate(Explosion, whereToPutExplosion, new Quaternion(0, 0, 0, 0));
        ExplosionQ.transform.localScale = new Vector2(rockScale, rockScale);
        ExplosionQ.GetComponent<Animator>().SetBool("isClicked", true);
        for (int i = 0; i < pieces.Length; i++)
        {
            Vector2 whereToPut = new Vector2(transform.position.x + Random.value, transform.position.y + Random.value);
            pieces[i].GetComponent<RockPiece>().rockScale = rockScale / 2;
            Instantiate(pieces[i], whereToPut, new Quaternion(0, 0, 0, 0));
        }
        StartCoroutine(RemoveExplosion(ExplosionQ, 0.5f));
        image.sprite = null;
        gameObject.GetComponent<Collider2D>().enabled = false;

    }
    IEnumerator RemoveExplosion(GameObject ExplosionQ, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(ExplosionQ);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.name == "Player" && !isHit)
        {
            // gameObject.GetComponent<Collider2D>().enabled = false;
            isHit = true;
            Player player = collision.collider.GetComponent<Player>();
            player.Hurt();
        }
        else
        {
            if ((!collision.collider.name.StartsWith("Rock")) || (Mathf.Abs(rb.velocity.y) < 1)) isHit = true;
        }

    }

    public void flyUp()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        rb.AddForce(new Vector2(0, (rb.mass * (500 + (Random.value * 200)))));
        rb.velocity = new Vector2(-(Random.value), 0);
        Invoke("ReEnableCollider", 0.5f);
    }

    private void ReEnableCollider()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
    }
}
