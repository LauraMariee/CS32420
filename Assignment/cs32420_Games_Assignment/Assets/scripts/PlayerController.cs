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

    private Rigidbody2D rigidbody;
    public Collider2D Collider2D;

    //Height of jump
    public float jumpForce = 5; 

    private Animator anim;

    public PlayerState player;

    public void Start(){
        anim = gameObject.GetComponent<Animator>();
        anim.SetTrigger("Idle");
        rigidbody = GetComponent<Rigidbody2D>(); 

    }

    /// <summary>
    /// Method which calculates the sprites movement based on speed.
    /// </summary>
    public void PlayerMovement()
    {
        float move = Input.GetAxis ("Horizontal");
        StateMachine();
        if (move < 0.0f)
        {
            anim.SetTrigger("WalkRight");
            rigidbody.velocity = new Vector2 ( move * playerSpeed, rigidbody.velocity.y);// Move the character by finding the target velocity
            //TODO: Fix angular velocity
            UnityEngine.Debug.Log("PlayerController PlayerMovement WalkRight");

        }
        else if (move > 0.0f)
        {
            rigidbody.velocity = new Vector2(move * playerSpeed, rigidbody.velocity.y);// Move the character by finding the target velocity
            UnityEngine.Debug.Log("PlayerController PlayerMovement WalkLeft");
        }
        else
        {
            anim.SetTrigger("Idle");
            UnityEngine.Debug.Log("PlayerController PlayerMovement Idle");
        }
        Jump(); 
    }


    public void Jump()
    {
        if (Input.GetButtonDown("Jump") && player == PlayerState.GROUND)
        {
            rigidbody.velocity = new Vector2(rigidbody.position.y, jumpForce);
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
        //StateMachine(); 
        PlayerMovement();
    }
}
