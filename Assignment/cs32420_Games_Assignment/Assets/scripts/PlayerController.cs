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
    public LayerMask WinLayer; 

    //Player State
    public enum PlayerState
    {
        JUMP,
        GROUND,
        CLIMB,
        GAMEOVER, 
        WIN
    };

    private Rigidbody2D rigidbody;
    public Collider2D Collider2D;

    //Height of jump
    public float jumpForce = 5; 

    private Animator anim;

    public PlayerState player;

    public void Start(){
        gameWon = false;
        anim = gameObject.GetComponent<Animator>();
        anim.SetTrigger("Idle");
        rigidbody = GetComponent<Rigidbody2D>(); 
    }

    public bool gameWon; 

    /// <summary>
    /// Method which calculates the sprites movement based on speed.
    /// </summary>
    public void PlayerMovement()
    {
        float move = Input.GetAxis("Horizontal");
        
        StateMachine();
        

        if (move > 0.0f)
        {
            anim.SetTrigger("WalkRight");
            rigidbody.velocity = new Vector2(move * playerSpeed, rigidbody.velocity.y);// Move the character by finding the target velocity
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

        if(player == PlayerState.CLIMB)
        {
            Climb();
        }
            
    }

    public void Jump()
    {
        player = PlayerState.JUMP;
        anim.SetTrigger("Jump Pressed");
        rigidbody.AddForce(Vector2.up * jumpForce); 
        //UnityEngine.Debug.Log("PlayerController PlayerMovement Jump");
    }

    public void Climb()
    {
        anim.SetTrigger("Climb");
        if (Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow))
        {
            rigidbody.velocity = new Vector2(0, playerSpeed);
        }
        else if(Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow)){
            rigidbody.velocity = new Vector2(0, -playerSpeed);
        }
        
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
            //UnityEngine.Debug.Log("PlayerController StateMachine CLIMB");
            player = PlayerState.CLIMB;
        }
        if (Collider2D.IsTouchingLayers(WinLayer))
        {
            UnityEngine.Debug.Log("PlayerController StateMachine WIN");
            player = PlayerState.WIN;
            gameWon = true; 
        }
        
    }


    /// <summary>"
    /// Calls movement methods
    /// </summary>
    public void Update(){ 
        PlayerMovement();
    }
}
