using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private float m_JumpForce = 100f;                          // Amount of force added when the player jumps.
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_SlideDisableCollider;                // A collider that will be disabled when sliding
    [SerializeField] private Collider2D m_SlideEnableCollider;                // A frontbody collider that will be enabled when sliding

    const float k_GroundedRadius = .5f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded = true;            // Whether or not the player is grounded.
    // const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    public bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    public CameraFollow cam;
    private Collider2D[] colliders;
    private bool isJumping = false;
    public bool isSliding = false;
    private bool canDoAction = true;
    private bool shouldCheckGround = true;
    public float targetSpeedMultiplier = 10f;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;
    public UnityEvent OnSlideEvent;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnSlideEvent == null)
            OnSlideEvent = new UnityEvent();
    }

    private void Update()
    {
        if (shouldCheckGround == true) IsGrounded();
        if (isJumping && m_Grounded) // if done jumping
        {
            isJumping = false;
            canDoAction = true;
            OnLandEvent.Invoke();
        }
    }

    public void IsGrounded()
    {
        colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        if (colliders.Length > 1)
        {
            m_Grounded = true;
        }
        else
        {
            m_Grounded = false;
        }
    }

    public void Move(float move, bool slide, bool jump)
    {

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * targetSpeedMultiplier, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }

        // If the player should jump...
        if (m_Grounded && jump && canDoAction)
        {
            m_Grounded = false;
            shouldCheckGround = false;  // give time to fly off the ground
            canDoAction = false;
            isJumping = true;
            Player.GetComponent<Player>().animator.SetBool("isJumping", true);
            m_Rigidbody2D.mass += 50;
            m_Rigidbody2D.AddForce(new Vector2(0f, (m_Rigidbody2D.mass * m_JumpForce)));
            Invoke("DoneJumping", 0.2f);
        }

        // If the player should slide...
        if (m_Grounded && slide && canDoAction)
        {
            canDoAction = false;
            isSliding = true;
            Player.GetComponent<Player>().animator.SetBool("isSliding", true);
            m_Rigidbody2D.mass += 10;
            //m_Rigidbody2D.AddForce(new Vector2((600), 1));
            targetSpeedMultiplier *= 2; // Little Boost of speed to make slide forward more fun
            Invoke("TargetSpeedRollBack", 0.3f); // Roll back the speed to normal

            // Disable one of the colliders when crouching and enable front collider
            if (m_SlideDisableCollider != null && m_SlideEnableCollider != null)
            {
                m_SlideDisableCollider.enabled = false;
                m_SlideEnableCollider.enabled = true;
            }
            Invoke("DoneSliding", 1.3f);
        }
    }

    private void DoneJumping()
    {
        m_Rigidbody2D.mass -= 50;
        shouldCheckGround = true; // as target flew away, now we can check if landed
    }

    private void DoneSliding()
    {
        canDoAction = true;
        isSliding = false;
        m_Rigidbody2D.mass -= 10;

        // Place colliders in default position when done sliding
        if (m_SlideDisableCollider != null && m_SlideEnableCollider != null)
        {
            m_SlideDisableCollider.enabled = true;
            m_SlideEnableCollider.enabled = false;
        }

        OnSlideEvent.Invoke();
    }

    private void TargetSpeedRollBack()
    {
        targetSpeedMultiplier /= 2;
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}