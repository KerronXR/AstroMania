﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public delegate void DiedOnLavaAction();
    public static event DiedOnLavaAction DiedOnLava;
    public delegate void WinLevelAction();
    public static event WinLevelAction WinLevel;
    public PlayerController controller;
    public Animator animator;
    public Rigidbody2D rb;
    public GameObject[] rockPrefab;
    public GameObject coreCounter;
    public GameObject boostButton;
    public GameObject timer;
    public GameObject thermometer;
    private GameObject ppFX;
    private GameObject dieCanvas;
    public Collider2D bottomCollider;
    public Collider2D slideCollider;
    private int pickRock;
    private bool isBusyCreatingPrefabRocks = false;
    public bool isTraining = false;
    private float horizontalMove = 0f;
    // private int healthPoints = 100;
    private int coreAmount = 0;
    private int aquiredCoreAmount = 0;
    public float runSpeed = 40.0f;
    private float defaultRunSpeed = 0;
    private float currentRunSpeed = 0;
    private float currentAnimationSpeed = 1;
    private float defaultAnimationSpeed = 1;
    bool jump = false;
    bool slide = false;
    bool boost = false;
    private Vector2 touchStartPos;
    private Vector2 touchDirection;
    public int numOfBoosts = 0;
    private int numOfHurts = 0;
    private bool shouldJump = false;
    private float previousXPosition;
    private bool checkPosDone = true;
    public float rockCreateMultiplier = 1.5f; // the lower the more rocks per second
    private int lavaLevel = 0;
    private bool isStandingOnLava = false;
    private bool alreadyDying = false;
    private bool isPlayerDead = false;


    private void Awake()
    {
        defaultRunSpeed = runSpeed;
        currentRunSpeed = runSpeed;
        ppFX = GameObject.Find("PostProcessingFX");
        dieCanvas = GameObject.Find("DiedOnLavaCanvas");
    }

    void Update()
    {

        if (rb.velocity.y > 17f)
        {
            Debug.Log(rb.velocity.y);
            rb.velocity = new Vector2(17f, rb.velocity.x);
        }
        if (checkPosDone)
        {
            previousXPosition = rb.position.x;
            checkPosDone = false;
            Invoke("checkPos", 0.1f);
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
                    touchDirection = new Vector2(0, 0);
                    break;
            }


            if (touchDirection.y > 50)
            {
                jump = true;
            }

            if (touchDirection.y < -100)
            {
                slide = true;
            }

        }

        if (Input.GetButtonDown("Slide"))
        {
            slide = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;

        }

        if (shouldJump)
        {
            jump = true;
        }

        checkTemperature();

        if (alreadyDying)
        {
            jump = false;
            slide = false;
        }
        // horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        horizontalMove = 1 * runSpeed;
        //horizontalMove = 17;
        controller.Move(horizontalMove * Time.fixedDeltaTime, slide, jump);
        jump = false;
        slide = false;

        if (!isBusyCreatingPrefabRocks && !isTraining) // only execute within 0-2 seconds random 
        {
            isBusyCreatingPrefabRocks = true;
            Invoke("CreatePrefabRocks", (Random.value * rockCreateMultiplier));
        }
    }

    public void checkPos() // Update the animator speed
    {
        animator.SetFloat("Speed", rb.position.x - previousXPosition);
        checkPosDone = true;
    }

    public void CreatePrefabRocks()
    {
        pickRock = (int)(Mathf.Floor(Random.value * rockPrefab.Length));
        Vector2 whereToPutRock = new Vector2((transform.position.x + ((Random.value * 15))), (transform.position.y + 7 + (Random.value * 2)));
        Instantiate(rockPrefab[pickRock], whereToPutRock, new Quaternion(0, 0, 0, 0));
        isBusyCreatingPrefabRocks = false;
    }
    public void onDoneSliding() // On stop sliding event invoke this
    {
        animator.SetBool("isSliding", false);
    }

    public void OnLanding() // On landing event invoke this
    {
        animator.SetBool("isJumping", false);
    }

    public void buttonUp()
    {
        shouldJump = true;
        Invoke("buttonUpDone", 0.2f);
    }

    public void buttonUpDone()
    {
        shouldJump = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        if (slideCollider.enabled == true && !collision.collider.name.StartsWith("Rock"))
        {
            foreach (ContactPoint2D cp in collision.contacts)
            {
                if(Mathf.Abs(cp.normal.y) < 0.25f)
                {
                    Debug.Log(cp.normal.y);
                    rb.position += new Vector2(0.2f, 0.5f);
                }
            }
           // if (collision.collider.bounds.max.y - slideCollider.bounds.min.y > 0);
        }
        */
        if (collision.collider.name == "Spaceship")
        {
            if (WinLevel != null) WinLevel();
        }
        if (collision.collider.name.StartsWith("Rock") && (controller.isSliding || boost))
        {
            Rock rock = collision.collider.GetComponent<Rock>();
            rock.flyUp();
        }
        if (collision.collider.name.Contains("small") && (controller.isSliding || boost))
        {
            RockPiece rockPiece = collision.collider.GetComponent<RockPiece>();
            rockPiece.flyUp();
        }
    }

    private void HurtStop()
    {
        numOfHurts--;
        if (numOfHurts < 1) // check if player wasn't hit again before stoping the slow effect
        {
            // animator.SetBool("isHurt", false);
            //runSpeed = currentRunSpeed;
            // animator.SetFloat("SlowDownMultiplier", currentAnimationSpeed);
        }
    }


    public void Hurt()
    {
        if (numOfBoosts < 1 && controller.isSliding == false)
        { // if player not Boosted or sliding then hit him
          // numOfHurts++;
            rb.AddForce(new Vector2(-30 * rb.mass, -10), ForceMode2D.Impulse);
            // runSpeed = currentRunSpeed / 2;
            // animator.SetFloat("SlowDownMultiplier", currentAnimationSpeed / 2);
            // Invoke("HurtStop", 1.5f);
        }
    }

    public void AddCore()
    {
        coreAmount++;
        AudioManager.instance.Play("GetCore");
        if (aquiredCoreAmount < 9)
        {
            aquiredCoreAmount++;
            boostButton.GetComponent<BoostButton>().SetBoostImage(aquiredCoreAmount);
        }
        coreCounter.GetComponent<CoresCounter>().SetCoresAmount(coreAmount);
        if (aquiredCoreAmount == 9)
        {
            AudioManager.instance.Play("BoostReady");
            boostButton.GetComponent<BoostButton>().AnimateReady(true);
        }


    }

    public void Boost()
    {
        if (aquiredCoreAmount == 9)
        {
            ppFX.GetComponent<Animator>().SetBool("boostOn", true);
            rb.mass += 50;
            boost = true;
            AudioManager.instance.Play("Boost");
            numOfBoosts++;
            runSpeed = defaultRunSpeed * 1.4f; // if speed is slowed - this release it
            currentRunSpeed = runSpeed;
            animator.SetFloat("SlowDownMultiplier", 1.4f);
            boostButton.GetComponent<BoostButton>().AnimateReady(false);
            currentAnimationSpeed = 1.4f;
            aquiredCoreAmount = 0;
            boostButton.GetComponent<BoostButton>().SetBoostImage(0);
            Invoke("BoostOff", 7f);
        }
    }

    public void BoostOff()
    {
        numOfBoosts--;
        if (numOfBoosts < 1)  // check if no more boosts were added before stopping boost
        {
            boost = false;
            rb.mass -= 50;
            runSpeed = defaultRunSpeed;
            currentRunSpeed = runSpeed;
            animator.SetFloat("SlowDownMultiplier", defaultAnimationSpeed);
            currentAnimationSpeed = defaultAnimationSpeed;
            ppFX.GetComponent<Animator>().SetBool("boostOn", false);
        }

    }

    public void StandsOnLava()
    {
        lavaLevel += 2;
        isStandingOnLava = true;
    }

    public void StopStandingOnLava()
    {
        isStandingOnLava = false;
    }

    private void checkTemperature()
    {
        if (isStandingOnLava)
        {
            if (lavaLevel >= 550 && lavaLevel < 950)
            {
                if (!alreadyDying) // run only once when starting to die
                {
                    rb.velocity = new Vector2(0, 0);
                    runSpeed = 0;
                    if (DiedOnLava != null) DiedOnLava(); // register death on level
                    alreadyDying = true;
                    Collider2D[] playerColliders = new Collider2D[10];
                    int contactCount = rb.GetContacts(playerColliders);
                    for (int i = 0; i < contactCount; i++)   // destroy all the rocks around
                    {
                        if (playerColliders[i].name.Contains("Rock") || playerColliders[i].name.Contains("small"))
                        {
                            Destroy(playerColliders[i].gameObject);
                        }
                    }
                    thermometer.GetComponent<Thermometer>().AnimateThermoBlow();                  
                    dieCanvas.GetComponent<GameOverDieOnLava>().LoadGameOverDieOnLavaCanvas();
                    ppFX.GetComponent<Animator>().SetBool("isWasted", true);
                    rb.isKinematic = true;
                }
                DieOnLava();
            }
            if (lavaLevel < 548)
            {
                thermometer.GetComponent<Thermometer>().SetThermo(lavaLevel / 10);
            }
            if (lavaLevel >= 950)
            {
                if (!isPlayerDead)
                {
                    foreach (Collider2D c in GetComponents<Collider2D>())
                    {
                        c.enabled = false;
                    }
                    rb.gravityScale = 0;
                    isPlayerDead = true;
                }

            }

        }
        else
        {
            if (lavaLevel > 0) lavaLevel--;
            if (lavaLevel == 0) thermometer.GetComponent<Thermometer>().SetThermo(0);
            if (lavaLevel < 548 && lavaLevel > 0)
            {
                thermometer.GetComponent<Thermometer>().SetThermo(lavaLevel / 10);
            }
        }
    }

    private void DieOnLava()
    {
        if (Time.timeScale > 0.31f) Time.timeScale -= 0.01f;
        Vector3 desiredPosition = new Vector3(transform.position.x, -7.8f, transform.position.z);
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, 2.25f * Time.deltaTime);
        transform.position = smoothPosition;
    }
}
