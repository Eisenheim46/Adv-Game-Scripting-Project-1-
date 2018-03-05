using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{

    // Player Properties ----
    [SerializeField] private int playerNumber;

    [SerializeField] private float coolDownSeconds; //Cool down in seconds before moving. Mainly for Slip()
    private float stampedTime; // To caputured time the moment it is used.
    private float groundDistance; //To check how far the player is from the ground

    private Collider collider; //To get player's collider component
    private Rigidbody player; //rigid body of the player (hand)

    //-----


    //For Player Inputs -----
    private float horizontalInput; //to get the player input ("Horizontal")
    private float verticalInput; //to get the player input ("Vertical")

    private string horizontalButtons;
    private string verticalButtons;
    private string jumpButton;
    private string fireButton;
    //-----


    //For Player Movements -----
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

    //-----


    // Use this for initialization
    void Awake ()
	{
        player = GetComponent<Rigidbody>(); //get the rigidbody component before using it
        collider = GetComponent<Collider>();//Get the player's collider component.
	}

    private void Start()
    {
        moveEnabled = true;
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

        if (moveEnabled) //Move is enabled when they are not jumping and after they slipped
            Move();

        if (Input.GetButtonDown(jumpButton) && isGrounded) //Jump when on the ground. Not while in the air.
            JumpSlap();

        if (Input.GetButtonDown(fireButton) && isGrounded && moveEnabled) //Slip should only happen on the ground
            Slip();


        Debug.Log(player.velocity.y);
	}


    private void Move()
    {
       // if ((Input.GetAxis(horizontalButtons) > 0 || Input.GetAxis(horizontalButtons) < 0) ||  ( Input.GetAxis(verticalButtons) > 0 || Input.GetAxis(verticalButtons) < 0))//(Input.GetButton("Horizontal1") || Input.GetButton("Vertical"))
        //{

            horizontalInput = Input.GetAxis(horizontalButtons); //Get the H axis on the input
            verticalInput = Input.GetAxis(verticalButtons); //Get the V axis on the input

            player.velocity = new Vector3(horizontalInput, 0, verticalInput) * speed; //Use Velocity for player movement * speed
        //}
    }

    private void JumpSlap()
    {
        moveEnabled = false; //Disable movement while jumping
        isGrounded = false; //Player is not on the ground

        player.velocity = new Vector3(jumpArch.x, jumpArch.y); //Use velocity to control angle of the jump

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
            case 3: break;
            case 4: break;
        }

    }


    private void EnableMovement()
    {


        if (IsGrounded) //Check if on the ground.
        {
            isGrounded = true;

            if (stampedTime <= Time.time) //Check if cooldown is complete.
                moveEnabled = true;
        }

    }



}
