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
    public GameObject coreCounter;
    public GameObject boostButton;
    private GameObject rockPrefab;
    private bool isBusyCreatingPrefabRocks = false;
    private float horizontalMove = 0f;
    private int healthPoints = 100;
    private int coreAmount = 0;
    private int aquiredCoreAmount = 0;
    public float runSpeed = 40.0f;
    private float defaultRunSpeed = 0;
    private float currentRunSpeed = 0;
    private float currentAnimationSpeed = 1;
    private float defaultAnimationSpeed = 1;
    bool jump = false;
    bool slide = false;
    private Vector2 touchStartPos;
    private float touchSlideLength;
    private Vector2 touchDirection;
    private int numOfBoosts = 0;
    private int numOfHurts = 0;

    private void Start()
    {
        defaultRunSpeed = runSpeed;
        currentRunSpeed = runSpeed;
    }

    void Update()
    {
        // horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        horizontalMove = 1 * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            controller.jumpFactor = 6;

        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    // Record initial touch position.
                    touchStartPos = touch.position;
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    // Determine direction by comparing the current touch position with the initial one
                    touchDirection = touch.position - touchStartPos;
                    break;

                case TouchPhase.Ended:
                    touchSlideLength = touchDirection.y;
                    touchDirection = new Vector2(0, 0);
                    break;
            }


            if (touchSlideLength > 50)
            {
                controller.jumpFactor = touchSlideLength / 100;
                jump = true;
                touchSlideLength = 0;
            }

            if (touchSlideLength < -100)
            {
                slide = true;
                touchSlideLength = 0;
            }

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

        if (Input.GetButtonDown("Slide"))
        {
            slide = true;
        }

        if (!isBusyCreatingPrefabRocks)
        {
            isBusyCreatingPrefabRocks = true;
            Invoke("CreatePrefabRocks", (Random.value * 2));
        }

        controller.Move(horizontalMove * Time.fixedDeltaTime, slide, jump);
        jump = false;
        slide = false;
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
    public void onDoneSliding()
    {
        animator.SetBool("isSliding", false);
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
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
        numOfHurts--;
        if (numOfHurts < 1)
        {
            // animator.SetBool("isHurt", false);
            runSpeed = currentRunSpeed;
            animator.SetFloat("SlowDownMultiplier", currentAnimationSpeed);
        }
    }


    public void Hurt()
    {
        numOfHurts++;
        rb.AddForce(new Vector2(-40, -0.1f), ForceMode2D.Impulse);
        runSpeed = currentRunSpeed / 2;
        animator.SetFloat("SlowDownMultiplier", currentAnimationSpeed / 2);
        Invoke("HurtStop", 1.5f);
    }


    public void AddCore()
    {
        coreAmount++;
        if (aquiredCoreAmount < 9)
        {
            aquiredCoreAmount++;
            boostButton.GetComponent<BoostButton>().SetBoostImage(aquiredCoreAmount);
        }
        coreCounter.GetComponent<CoresCounter>().SetCoresAmount(coreAmount);
        if (aquiredCoreAmount == 9) 
        { 
            boostButton.GetComponent<BoostButton>().animator.SetBool("isReady", true); 
        }


    }

    public void Boost()
    {
        if (aquiredCoreAmount == 9)
        {
            numOfBoosts++;
            runSpeed = defaultRunSpeed * 1.5f;
            currentRunSpeed = runSpeed;
            animator.SetFloat("SlowDownMultiplier", 1.5f);
            boostButton.GetComponent<BoostButton>().animator.SetBool("isReady", false);
            currentAnimationSpeed = 1.5f;
            aquiredCoreAmount = 0;
            boostButton.GetComponent<BoostButton>().SetBoostImage(0);
            Invoke("BoostOff", 5f);
        }
    }

    public void BoostOff()
    {
        numOfBoosts--;
        if (numOfBoosts < 1)
        {
            runSpeed = defaultRunSpeed;
            currentRunSpeed = runSpeed;
            animator.SetFloat("SlowDownMultiplier", defaultAnimationSpeed);
            currentAnimationSpeed = defaultAnimationSpeed;
        }

    }

}
