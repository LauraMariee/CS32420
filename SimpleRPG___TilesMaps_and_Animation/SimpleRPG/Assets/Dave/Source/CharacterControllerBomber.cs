using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerBomber : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    public bool facingRight = true;

    [Range(0, .3f)] [SerializeField] private float MovementSmoothing = .05f;    // How much to smooth out the movement
    [SerializeField] private float JumpForce = 400f;                            // Amount of force added when the player jumps.
    [SerializeField] private float MoveForce = 10f;                            // Amount of force added when the player jumps.

    [SerializeField] public LayerMask GroundLayer;							// A mask determining what is ground to the character

    private Vector3 currentVelocity = Vector3.zero;
    private enum FlightStatus { Grounded, Ballistic, JumpPrepare, Jump };
    private FlightStatus flightStatus = FlightStatus.Ballistic;

    public CircleCollider2D GroundCheckCollider = null;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (!GroundCheckCollider)
            GroundCheckCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown("space") && flightStatus == FlightStatus.Grounded)        // key down so prepare jump
        {
            flightStatus = FlightStatus.Jump;
            animator.SetTrigger("Jump");
        }

        Move(h);
    }

    private void FixedUpdate()
    {
        if(GroundCheckCollider.IsTouchingLayers(GroundLayer)) // Is ground check collider touching an object in the ground layer?
        {
            Debug.Log("Upward Grounded Applied");
            flightStatus = FlightStatus.Grounded;
            animator.SetBool("Grounded",true);
        }
        else
        {
            animator.SetBool("Grounded", false);
        }
    }

    public void Move(float move)
    {
         //only control the player if grounded or airControl is turned on
        if (flightStatus==FlightStatus.Grounded || flightStatus == FlightStatus.Ballistic)
        {

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * MoveForce, rb.velocity.y);
            // And then smoothing it out and applying it to the character
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, MovementSmoothing);

            animator.SetFloat("HSpeed", Mathf.Abs(rb.velocity.x));
            animator.SetFloat("VSpeed", rb.velocity.y);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !facingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && facingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if(flightStatus == FlightStatus.Jump)
        {
            // Add a vertical force to the player.
            flightStatus = FlightStatus.Ballistic;
            Debug.Log("Upward Force Applied");
            rb.AddForce(new Vector2(0f, JumpForce));
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
