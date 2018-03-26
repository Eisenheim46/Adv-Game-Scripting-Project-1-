using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JoinMenu : MonoBehaviour {

    [SerializeField] private Image[] joinImage = new Image[4];

    private bool[] hasJoined = new bool[4];

    private int playerNumber;
    private int playerNumberIndex;

    private void Update()
    {
        if (Input.GetButtonDown("Submit1"))
        {
            playerNumber = 1;//This is player 1 
            playerNumberIndex = playerNumber - 1;

            hasJoined[playerNumberIndex] = !hasJoined[playerNumberIndex]; //Inverse everytime they press start
            JoinMenuProperties.joinMenuProperties.SetJoinInfo(playerNumber, hasJoined[playerNumberIndex]); //Record it in the properties script

            ChangeTransparency();
        }
        else if (Input.GetButtonDown("Submit2"))
        {
            playerNumber = 2;//This is player 2
            playerNumberIndex = playerNumber - 1;

            hasJoined[playerNumberIndex] = !hasJoined[playerNumberIndex]; //Inverse everytime they press start
            JoinMenuProperties.joinMenuProperties.SetJoinInfo(playerNumber, hasJoined[playerNumberIndex]); //Record it in the properties script

            ChangeTransparency();
        }
        else if (Input.GetButtonDown("Submit3"))
        {
            playerNumber = 3;//This is player 3
            playerNumberIndex = playerNumber - 1;

            hasJoined[playerNumberIndex] = !hasJoined[playerNumberIndex]; //Inverse everytime they press start
            JoinMenuProperties.joinMenuProperties.SetJoinInfo(playerNumber, hasJoined[playerNumberIndex]); //Record it in the properties script

            ChangeTransparency();
        }
        else if (Input.GetButtonDown("Submit4"))
        {
            playerNumber = 4;//This is player 4
            playerNumberIndex = playerNumber - 1;

            hasJoined[playerNumberIndex] = !hasJoined[playerNumberIndex]; //Inverse everytime they press start
            JoinMenuProperties.joinMenuProperties.SetJoinInfo(playerNumber, hasJoined[playerNumberIndex]); //Record it in the properties script

            ChangeTransparency();
        }

        if (Input.GetButtonDown("Start"))
        {
            SceneManager.LoadScene(2);
        }

    }

    private void ChangeTransparency()
    {
        //Change the alpha in the join images
        if (hasJoined[playerNumberIndex] == true)
        {
            Color newColor = joinImage[playerNumberIndex].color; //Set Color (Vector 3) variable equal joinImage's current Color

            newColor.a = 0.5f; //Change only the alpha

            joinImage[playerNumberIndex].color = newColor; //Set joinImage's color with the new Vector 3.

        }
        else if (hasJoined[playerNumberIndex] == false)
        {
            Color newColor = joinImage[playerNumberIndex].color; //Set Color (Vector 3) variable equal joinImage's current Color

            newColor.a = 1f; //Change only the alpha

            joinImage[playerNumberIndex].color = newColor; //Set joinImage's color with the new Vector 3.
        }
    }

}
