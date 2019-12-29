using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject MainPlayer;
    [SerializeField] private float m_JumpForce = 100f;                          // Amount of force added when the player jumps.
    [Range(1, 3)] [SerializeField] private float m_SlideSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_SlideDisableCollider;                // A collider that will be disabled when sliding
    [SerializeField] private Collider2D m_SlideEnableCollider;                // A frontbody collider that will be enabled when sliding

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded = true;            // Whether or not the player is grounded.
    // const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    public bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    public CameraFollow cam;
    private Collider2D[] colliders;
    private bool wasFlying = false;
    private bool isJumping = false;
    private bool isSliding = false;
    public float jumpFactor = 1;

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
        m_Grounded = false;
        colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        if (colliders.Length > 0)
        {
            m_Grounded = true;
            if (wasFlying)
            {
                wasFlying = false;
                //Debug.Log("Landed");
                //cam.offset.y = 2;
                OnLandEvent.Invoke();
            }
        }
        else
        {
            wasFlying = true;
        }
    }


    public void Move(float move, bool slide, bool jump)
    {

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
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
        if (m_Grounded && jump && !isJumping && !isSliding)
        {
            isJumping = true;
            MainPlayer.GetComponent<Player>().animator.SetBool("isJumping", true);
            m_Grounded = false;
            m_Rigidbody2D.mass = 50;
            m_Rigidbody2D.AddForce(new Vector2(0f, jumpFactor * (50 * m_JumpForce)));
            Invoke("DoneJumping", 0.1f);
        }

        // If the player should slide...
        if (m_Grounded && slide && !isJumping && !isSliding)
        {
            isSliding = true;
            MainPlayer.GetComponent<Player>().animator.SetBool("isSliding", true);
            m_Grounded = false;
            m_Rigidbody2D.mass = 50;
            m_Rigidbody2D.AddForce(new Vector2(30000f, 0));

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
        isJumping = false;
        m_Rigidbody2D.mass = 1;
    }

    private void DoneSliding()
    {
        isSliding = false;
        m_Rigidbody2D.mass = 1;
        m_Grounded = true;

        // Place colliders in default position when done sliding
        if (m_SlideDisableCollider != null && m_SlideEnableCollider != null)
        {
            m_SlideDisableCollider.enabled = true;
            m_SlideEnableCollider.enabled = false;
        }

        OnSlideEvent.Invoke();
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