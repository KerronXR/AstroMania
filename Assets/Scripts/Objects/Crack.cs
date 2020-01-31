using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crack : MonoBehaviour
{
    public Animator animator;
    public Collider2D holeCollider;
    public Collider2D triggerCollider;
    private bool isCracked = false;

    public void MakeCrack()
    {
        isCracked = true;
        Invoke("Crack1Stage", 0.5f);
    }

    private void Crack1Stage()
    {
        AudioManager.instance.Play("Stonefall" + Random.Range(1, 4));
        animator.SetBool("isCracking", true);
        Invoke("Crack2Stage", 0.4f);
    }

    private void Crack2Stage()
    {
        holeCollider.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "CrackCheck" && !isCracked)
        {
            if (Random.value > 0.7)
            {
                triggerCollider.enabled = false;
                Invoke("MakeCrack", 0.2f);
            }
        }
    }
}
