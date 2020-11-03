using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Play positioning enums
    public float playerSpeed = 10;

    //Declare Axis
    private float xAxis;
    private float yAxis;

    //Layers
    public LayerMask PlatformLayer;
    public LayerMask DeathLayer; 

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

    private float old_position;

    public void Start(){
        anim = gameObject.GetComponent<Animator>();
        anim.SetTrigger("Idle");
        old_position = transform.position.x;
    }

    /// <summary>
    /// Method which calculates the sprites movement based on speed.
    /// </summary>
    public void PlayerMovement()
    {
        float move = Input.GetAxis ("Horizontal");
        float moveUp = Input.GetAxis("Vertical");

        if (old_position <= this.GetComponent<Transform>().position.x)
        {
            Vector2 targetVelocity = new Vector2 ( move * playerSpeed, rigidBody.velocity.y);// Move the character by finding the target velocity
            rigidBody.velocity = targetVelocity;

            anim.SetTrigger("WalkRight");
            UnityEngine.Debug.Log("PlayerController PlayerMovement WalkRight");

            jumpMethod();

        }

        else
        {
            UnityEngine.Debug.Log("PlayerController PlayerMovement WalkLeft");

            Vector2 targetVelocity = new Vector2(move * playerSpeed, rigidBody.velocity.y);// Move the character by finding the target velocity
            rigidBody.velocity = targetVelocity;

            jumpMethod();
        }
    }


    public void jumpMethod()
    {
        if (Input.GetButtonDown("Jump") && player == PlayerState.GROUND)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
            anim.SetTrigger("Jump Pressed");
            player = PlayerState.JUMP;
            UnityEngine.Debug.Log("PlayerController PlayerMovement Jump");
        }
    }


    public void StateMachine()
    {
        if (Collider2D.IsTouchingLayers(PlatformLayer))
        {
            player = PlayerState.GROUND;
        }
        if (Collider2D.IsTouchingLayers(DeathLayer))
        {
            player = PlayerState.DEATH;
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
