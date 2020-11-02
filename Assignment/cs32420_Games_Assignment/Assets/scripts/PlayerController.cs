using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Play positioning enums
    public float playerSpeed = 50;

    //Declare Axis
    private float xAxis;
    private float yAxis;

    //Platform Layer
    public LayerMask PlatformLayer;


    //Player State
    public enum PlayerState
    {
        JUMP,
        GROUND,
        CLIMB,
        DEATH
    };

    //Rigidbody
    public Rigidbody2D rigidBody;
    public Collider2D Collider2D;

    //Height of jump
    public float jumpForce = 5; 

    private Animator anim;

    public PlayerState player; 

    public void Start(){
        anim = gameObject.GetComponent<Animator>();
        anim.SetTrigger("Idle"); 
    }

    /// <summary>
    /// Method which calculates the sprites movement based on speed.
    /// </summary>
    public void PlayerMovement()
    {
        float move = Input.GetAxis ("Horizontal");
        
        Vector3 targetVelocity = new Vector2 ( move * playerSpeed, rigidBody.velocity.y);// Move the character by finding the target velocity
        rigidBody.velocity = targetVelocity;

        anim.SetTrigger("WalkRight");
        
        if (Input.GetButtonDown("Jump") && player == PlayerState.GROUND)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce); 
                anim.SetTrigger("Jump Pressed");
                player = PlayerState.JUMP;
                Debug.Log("PlayerController PlayerMovement Jump");
            }      
    }


    public void StateMachine()
    {
        if (Collider2D.IsTouchingLayers(PlatformLayer))
        {
            player = PlayerState.GROUND;
        }
    }


    /// <summary>"
    /// Calls movement methods
    /// </summary>
    public void Update(){
        StateMachine(); 
        PlayerMovement();
    }
}
