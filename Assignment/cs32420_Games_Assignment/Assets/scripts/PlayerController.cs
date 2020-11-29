using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

public class PlayerController : MonoBehaviour
{
    //Play positioning enums
    private float playerSpeed = 5;

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

    private new Rigidbody2D rigidbody; 
    private new Collider2D collider2D;

    //Height of jump
    public float jumpForce = 5;

    //player animator object
    private Animator anim;

    private PlayerState playerState;
    private readonly TimeTravel<Vector2> timeTravel = new TimeTravel<Vector2>();

    public bool gameWon;
    public bool gameLost;


    public void Start()
    {
        gameWon = false;
        gameLost = false;
        anim = gameObject.GetComponent<Animator>();
        anim.SetTrigger("Idle");
        rigidbody = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>(); 
    }

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
            rigidbody.velocity =
                new Vector2(move * playerSpeed,
                    rigidbody.velocity.y); // Move the character by finding the target velocity
            //UnityEngine.Debug.Log("PlayerController PlayerMovement WalkRight");
        }
        else if (move < 0.0f)
        {
            rigidbody.velocity =
                new Vector2(move * playerSpeed,
                    rigidbody.velocity.y); // Move the character by finding the target velocity
            rigidbody.angularVelocity = 0;
            //UnityEngine.Debug.Log("PlayerController PlayerMovement WalkLeft");
        }
        else
        {
            anim.SetTrigger("Idle");
            //UnityEngine.Debug.Log("PlayerController PlayerMovement Idle");
        }

        if (Input.GetButtonDown("Jump") && playerState == PlayerState.GROUND)
        {
            Jump();
        }

        if (playerState == PlayerState.CLIMB)
        {
            Climb();
        }
    }

    public void Jump()
    {
        playerState = PlayerState.JUMP;
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
        else if (Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow))
        {
            rigidbody.velocity = new Vector2(0, -playerSpeed);
        }
    }


    /// <summary>
    /// Changes player state depending on layer collision
    /// </summary>
    public void StateMachine()
    {
        if (collider2D.IsTouchingLayers(PlatformLayer))
        {
            //UnityEngine.Debug.Log("PlayerController StateMachine GROUND");
            playerState = PlayerState.GROUND;
        }

        if (collider2D.IsTouchingLayers(GameOverLayer))
        {
            //UnityEngine.Debug.Log("PlayerController StateMachine GAMEOVER");
            anim.SetTrigger("GameOver");
            playerState = PlayerState.GAMEOVER;
            gameLost = true;
        }

        if (collider2D.IsTouchingLayers(LadderLayer))
        {
            //UnityEngine.Debug.Log("PlayerController StateMachine CLIMB");
            playerState = PlayerState.CLIMB;
        }

        if (collider2D.IsTouchingLayers(WinLayer))
        {
            UnityEngine.Debug.Log("PlayerController StateMachine WIN");
            playerState = PlayerState.WIN;
            gameWon = true;
        }
    }


    /// <summary>"
    /// Calls movement methods
    /// </summary>
    public void Update()
    {
        if (!timeTravel.IsTravellingBack)
        {
            MovePlayer();
        }
        else
        {
            MovePlayerBackThroughTime();
        }
    }

    private void MovePlayer()
    {
        PlayerMovement();
        timeTravel.CaptureState(rigidbody.position);
    }

    private void MovePlayerBackThroughTime()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0;
        var ttPosition = timeTravel.GetNextPastFrame();
        if (ttPosition.HasValue)
        {
            rigidbody.position = ttPosition.Value;
        }
    }


    public void TriggerTimeTravel(int duration)
    {
        timeTravel.StartToTravelBack(duration);
    }
}