using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;

    void Start()
    {
        animator.SetBool("isFinish", true);
    }

}