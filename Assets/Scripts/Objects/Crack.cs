using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crack : MonoBehaviour
{
    public Animator animator;
    public GameObject Timer;
    public Collider2D holeCollider;
    private bool isHit = false;

    public void MakeCrack()
    {
        Invoke("Crack1Stage", 0.5f);
    }

    private void Crack1Stage()
    {
        animator.SetBool("isCracking", true);
        Invoke("Crack2Stage", 0.4f);
    }

    private void Crack2Stage()
    {
        holeCollider.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Player" && !isHit)
        {          
            isHit = true;
            Player player = collider.GetComponent<Player>();
            player.HasFallenDown();
            Invoke("CanReEnter", 0.8f);
        }
    }

    void CanReEnter()
    {
        isHit = false;
    }
}
