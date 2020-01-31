using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockWall : MonoBehaviour
{
    public SpriteRenderer image;
    public GameObject Explosion;
    public GameObject[] pieces;
    private float rockScale;
    public static bool queriesHitTriggers;

    private void Start()
    {
        queriesHitTriggers = false;
        rockScale = 0.7f + Random.value * 0.7f;
        transform.localScale = new Vector2(rockScale, rockScale);
    }
    private void OnMouseDown()
    {
        Vector2 whereToPutExplosion = new Vector2(transform.position.x, transform.position.y);
        GameObject ExplosionQ = Instantiate(Explosion, whereToPutExplosion, new Quaternion(0, 0, 0, 0));
        ExplosionQ.transform.localScale = new Vector2(rockScale, rockScale);
        ExplosionQ.GetComponent<Animator>().SetBool("isClicked", true);
        AudioManager.instance.PlayBlast();
        for (int i = 0; i < pieces.Length; i++)
        {
            Vector2 whereToPut = new Vector2(transform.position.x + Random.value, transform.position.y + Random.value);
            pieces[i].GetComponent<RockPiece>().rockScale = rockScale;
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
}
