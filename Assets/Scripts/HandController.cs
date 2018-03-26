using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{

    //Player Properties ----
    [SerializeField] private int playerNumber;
    private int scorePoints;

    [SerializeField] private float coolDownSeconds; //Cool down in seconds before moving. Mainly for Slip()
    private float stampedTime; // To caputured time the moment it is used.
    private float groundDistance; //To check how far the player is from the ground

    private Collider collider; //To get player's collider component
    private Rigidbody player; //rigid body of the player (hand)
    private AudioSource clapSFX;
    //-----


    //Player Inputs -----
    private float horizontalInput; //to get the player input ("Horizontal")
    private float verticalInput; //to get the player input ("Vertical")

    private string horizontalButtons;
    private string verticalButtons;
    private string jumpButton;
    private string fireButton;
    //-----


    //Player Movements -----
    [SerializeField] private float speed; //speed of the player (hand) movements
    [SerializeField] private float slipOffsetAmount;//How far to push the player back shen slipping

    [SerializeField] private Vector2 jumpArch; //How far x and how far y when jumping

    private Vector3 slipOffset; //To convert the slipOffsetAmount into a Vector 3 later;

    private bool moveEnabled; //To allow movement or not
    private bool isGrounded; //Check if on the ground *Avoid double jump 
    //-----

    //Properties -----
    private bool IsGrounded
    {
        get { return Physics.Raycast(transform.position, -Vector3.up, groundDistance + 0.01f); }
    }

    public int PlayerNumber { get { return playerNumber; } }
    //-----


    // Use this for initialization
    void Awake ()
	{
        scorePoints = 0; //Every Player starts with 0 points

        player = GetComponent<Rigidbody>(); //get the rigidbody component before using it
        collider = GetComponent<Collider>();//Get the player's collider component.
        clapSFX = GetComponent<AudioSource>();//Get the audiosource component for the sfx.
    }

    private void Start()
    {
        moveEnabled = false;
        isGrounded = false;

        horizontalButtons = "Horizontal" + playerNumber;
        verticalButtons = "Vertical" + playerNumber;
        jumpButton = "Jump" + playerNumber;
        fireButton = "Fire" + playerNumber;

        groundDistance = collider.bounds.extents.y;
    }

    // Update is called once per frame
    void Update ()
	{
        EnableMovement();  //Will enable movement

        if (moveEnabled && isGrounded) //Move is enabled when they are not jumping and after they slipped
            Move();

        if (Input.GetButtonDown(jumpButton) && isGrounded) //Jump when on the ground. Not while in the air.
            JumpSlap();

        if (Input.GetButtonDown(fireButton) && isGrounded && moveEnabled) //Slip should only happen on the ground
            Slip();
    }


    private void Move()
    {
        horizontalInput = Input.GetAxis(horizontalButtons); //Get the H axis on the input
        verticalInput = Input.GetAxis(verticalButtons); //Get the V axis on the input

        if (Mathf.Abs(horizontalInput) > 0 || Mathf.Abs(verticalInput) > 0)
            player.velocity = new Vector3(horizontalInput, 0, verticalInput) * speed; //Use Velocity for player movement * speed

    }

    private void JumpSlap()
    {
        moveEnabled = false; //Disable movement while jumping
        isGrounded = false; //Player is not on the ground

        stampedTime = Time.time + coolDownSeconds;

        float newXVelocity = Input.GetAxis(horizontalButtons) * jumpArch.x;
        float newZVelocity = Input.GetAxis(verticalButtons) * jumpArch.x;

        player.velocity = new Vector3(newXVelocity, jumpArch.y, newZVelocity);
    }

    private void Slip()
    {
        moveEnabled = false;

        stampedTime = Time.time + coolDownSeconds;

        switch (playerNumber)
        {
            case 1:
                slipOffset = new Vector3(slipOffsetAmount, 0, 0);
                player.MovePosition(transform.position - slipOffset); //Slip to the left

                break;
            case 2:
                slipOffset = new Vector3(slipOffsetAmount, 0, 0);
                player.MovePosition(transform.position + slipOffset); //Slip to the Right

                break;
            case 3:
                slipOffset = new Vector3(0, 0, slipOffsetAmount);
                player.MovePosition(transform.position + slipOffset); //Slip Up

                break;
            case 4:
                slipOffset = new Vector3(0, 0, slipOffsetAmount);
                player.MovePosition(transform.position - slipOffset); //Slip Down
                break;
        }

    }


    private void EnableMovement()
    {


        if (IsGrounded) //Check if on the ground.
        {
            //Debug.Log("Grounded");
            if (stampedTime <= Time.time) //Check if cooldown is complete.
            {
                moveEnabled = true;
                isGrounded = true;
            }
        }
        else
        {
            moveEnabled = false;
            isGrounded = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        bool isAbove = this.transform.position.y > collision.transform.position.y;

        if (collision.gameObject.tag == "Floor" && isAbove)
        {
            clapSFX.PlayDelayed(0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isAbove = this.transform.position.y > other.transform.position.y;

        if (other.gameObject.tag == "Player" && isAbove)
        {
            scorePoints++;

            GameManager.gameManager.UpdateScore(playerNumber, scorePoints);

            DisablePlayer(other);

            GameManager.gameManager.DecreaseActivePlayers();
        }
        else if (other.gameObject.tag == "Floor" && isAbove)
        {
            clapSFX.PlayDelayed(0);
        }
    }

    private void DisablePlayer(Collider other)
    {
        other.gameObject.GetComponent<Collider>().enabled = false;
        other.gameObject.SetActive(false);
    }

}
