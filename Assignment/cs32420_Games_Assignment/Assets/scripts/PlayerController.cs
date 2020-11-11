using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    public LayerMask GameOverLayer;
    public LayerMask LadderLayer;

    //Player State
    public enum PlayerState
    {
        JUMP,
        GROUND,
        CLIMB,
        GAMEOVER
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
        

        if (move > 0.0f)
        {
            anim.SetTrigger("WalkRight");
            rigidbody.velocity = new Vector2(move * playerSpeed, rigidbody.velocity.y);// Move the character by finding the target velocity
            rigidbody.angularVelocity = 0;
            //TODO: Fix angular velocity
            //UnityEngine.Debug.Log("PlayerController PlayerMovement WalkRight");

        }
        else if (move < 0.0f)
        {
            rigidbody.velocity = new Vector2(move * playerSpeed, rigidbody.velocity.y);// Move the character by finding the target velocity
            rigidbody.angularVelocity = 0;
            //UnityEngine.Debug.Log("PlayerController PlayerMovement WalkLeft");
        }
        else
        {
            anim.SetTrigger("Idle");
            //UnityEngine.Debug.Log("PlayerController PlayerMovement Idle");
        }

        if (Input.GetButtonDown("Jump") && player == PlayerState.GROUND)
        {
            Jump();
        }
            
    }


    public void Jump()
    {
        player = PlayerState.JUMP;
        anim.SetTrigger("Jump Pressed");
        rigidbody.AddForce(Vector2.up * jumpForce); 
        //UnityEngine.Debug.Log("PlayerController PlayerMovement Jump");
    }

    /// <summary>
    /// Changes player state depending on layer collision
    /// </summary>
    public void StateMachine()
    {
        if (Collider2D.IsTouchingLayers(PlatformLayer))
        {
            //UnityEngine.Debug.Log("PlayerController StateMachine GROUND");
            player = PlayerState.GROUND;
        }
        if (Collider2D.IsTouchingLayers(GameOverLayer))
        {
            //UnityEngine.Debug.Log("PlayerController StateMachine GAMEOVER");
            anim.SetTrigger("GameOver");
            player = PlayerState.GAMEOVER;
        }
        if (Collider2D.IsTouchingLayers(LadderLayer))
        {
            UnityEngine.Debug.Log("PlayerController StateMachine CLIMB");
            anim.SetTrigger("Climb");
            player = PlayerState.CLIMB;
        }
    }


    /// <summary>"
    /// Calls movement methods
    /// </summary>
    public void Update(){ 
        PlayerMovement();
    }
}
