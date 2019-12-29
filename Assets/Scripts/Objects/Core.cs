using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    //public Animator animator;
    private bool isHit = false;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Player" && !isHit)
        {
            isHit = true;
            gameObject.GetComponent<Collider2D>().enabled = false;
            Player player = collider.GetComponent<Player>();
            player.AddCore();
            //  animator.SetBool("isTaken", true);
            Invoke("Die", 0.2f);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
