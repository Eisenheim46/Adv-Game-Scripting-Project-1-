using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{

    private Rigidbody player; //rigid body of the player (hand)

    //For Player Movements -----
    [SerializeField] private float speed; //speed of the player (hand) movements
    [SerializeField] private Vector2 jumpArch;


    private float horizontalInput; //to get the player input ("Horizontal")
    private float verticalInput; //to get the player input ("Vertical")

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
    }

    // Update is called once per frame
    void Update ()
	{
        if (player.velocity.y == 0)
        {
            moveEnabled = true;
            isGrounded = true;
        }


        if (moveEnabled)
            Move();

        if (Input.GetButtonDown("Jump") && isGrounded)
            JumpSlap();
       
	}


    private void Move()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            player.velocity = new Vector3(horizontalInput, 0, verticalInput) * speed;
        }
    }

    private void JumpSlap()
    {
        moveEnabled = false;
        isGrounded = false;

        player.velocity = new Vector3(jumpArch.x, jumpArch.y);

    }

}
