using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{

    [SerializeField] private int playerNumber;
    private Rigidbody player; //rigid body of the player (hand)

    //For Player Inputs -----
    private float horizontalInput; //to get the player input ("Horizontal")
    private float verticalInput; //to get the player input ("Vertical")

    private string horizontalButtons;
    private string verticalButtons;
    private string jumpButton;

    //-----


    //For Player Movements -----
    [SerializeField] private float speed; //speed of the player (hand) movements
    [SerializeField] private Vector2 jumpArch;

    private bool moveEnabled; //To allow movement or not
    private bool isGrounded; //Check if on the ground *Avoid double jump 
    //-----

    

	// Use this for initialization
	void Awake ()
	{
        player = GetComponent<Rigidbody>(); //get the rigidbody component before using it
       
	}

    private void Start()
    {
        moveEnabled = true;
        isGrounded = false;

        horizontalButtons = "Horizontal" + playerNumber;
        verticalButtons = "Vertical" + playerNumber;
        jumpButton = "Jump" + playerNumber;
    }

    // Update is called once per frame
    void Update ()
	{

        CheckGrounded();  //Check if on the ground. Will enable movement

        if (moveEnabled)
            Move();

        if (Input.GetButtonDown(jumpButton) && isGrounded)
            JumpSlap();
       
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

    private void CheckGrounded()
    {


        if (player.velocity.y == 0) //Will be refined by another technique
        {
            moveEnabled = true;
            isGrounded = true;
        }
    }

}
