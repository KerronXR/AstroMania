using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public Animator animator;
    public Rigidbody2D rb;
    public GameObject rockPrefab1;
    public GameObject rockPrefab2;
    public GameObject rockPrefab3;
    private GameObject rockPrefab;
    private bool isBusyCreatingPrefabRocks = false;
    private float horizontalMove = 0f;
    private int healthPoints = 100;
    public float runSpeed = 40.0f;
    bool jump = false;
    bool crouch = false;

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            //animator.SetBool("isJumping", true);

        }

        /*if (Input.GetButtonDown("Fire1"))
        {
            //animator.SetBool("isAttacking", true);
            if (controller.m_FacingRight == true)
                attackPoint.transform.position = new Vector2(transform.position.x + (float)1.5, transform.position.y - (float)0.5);
            else attackPoint.transform.position = new Vector2(transform.position.x - (float)1.5, transform.position.y - (float)0.5);

        }
        else if (Input.GetButtonUp("Fire1"))
        {
            //animator.SetBool("isAttacking", false);
            attackPoint.transform.position = new Vector2(transform.position.x, transform.position.y - 7);

        } */

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
            // animator.SetBool("isCrouching", true);

        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        if (!isBusyCreatingPrefabRocks)
        {
            isBusyCreatingPrefabRocks = true;
            Invoke("CreatePrefabRocks", (Random.value * 2));
        }
    }

    public void CreatePrefabRocks()
    {
        switch ((Mathf.Floor(Random.value * 3) + 1))
        {
            case 1:
                rockPrefab = rockPrefab1;
                break;
            case 2:
                rockPrefab = rockPrefab2;
                break;
            case 3:
                rockPrefab = rockPrefab3;
                break;
            default:
                break;
        }
        Vector2 whereToPutRock = new Vector2((transform.position.x + ((Random.value * 10))), (transform.position.y + 6 + (Random.value * 2)));
        Instantiate(rockPrefab, whereToPutRock, new Quaternion(0, 0, 0, 0));
        isBusyCreatingPrefabRocks = false;
    }
    public void OnCrouching(bool isCrouching)
    {
        // animator.SetBool("isCrouching", isCrouching);
    }

    public void OnLanding()
    {
        // animator.SetBool("isJumping", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Spaceship")
        {
            SceneManager.LoadScene("Win");
        }
    }

    private void HurtStop()
    {
        animator.SetBool("isHurt", false);
        runSpeed = runSpeed * 2;
        animator.SetFloat("SlowDownMultiplier", 1f);
    }


    public void Hurt()
    {
        rb.AddForce(new Vector2(-60, 0), ForceMode2D.Impulse);
        runSpeed = runSpeed / 2;
        animator.SetFloat("SlowDownMultiplier", 0.5f);
        Invoke("HurtStop", 1.5f);
    }
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;

    }

    void Restart()
    {
        transform.position = new Vector2(0, -20);
        // cam.transform.position = new Vector2(0, -20);
        Invoke("ReturnToLevel", 4f);
    }

    void ReturnToLevel()
    {
        SceneManager.LoadScene("Lv1");
    }
}
