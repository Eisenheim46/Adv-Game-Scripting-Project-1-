using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;

    [SerializeField] private Text roundText;
    [SerializeField] private Text[] playerScore; //Set up score text

    [SerializeField] private GameObject pauseMenu;

    //Handle Rounds

    [SerializeField] private GameObject players;

    private Vector3[] startingPosition = new Vector3[4];

    private int activePlayers;
    private int roundNumber;
    //


    private void Awake()
    {
        InstantiateClass();      
    }

    private void Start()
    {
        InitializeVariables();       
    }

    private void LateUpdate()
    {
        if (Input.GetButtonDown("Start"))
            pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void UpdateScore(int playerNumber, int scoreCard)
    {
        int playerIndex = playerNumber - 1; //To correct for Array Index

        playerScore[playerIndex].text = "Player " + playerNumber + "\n" + scoreCard;
    }

    public void UpdateRound()
    {
        roundNumber++;

        roundText.text = "Round: " + roundNumber;

        ResetPlayers();

        CountPlayers();
    }


    public void DecreaseActivePlayers()
    {
        activePlayers--;

        if (activePlayers <= 1)
        {
            UpdateRound();
        }
    }


    private void InstantiateClass()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            gameManager = this;
        }
    }

    private void InitializeVariables()
    {
        activePlayers = 0;
        roundNumber = 1;

        GetStartPosition();

        CountAndEliminatePlayers();

        roundText.text = "Round " + roundNumber;
    }

    private void CountAndEliminatePlayers()
    {
        int counter = 0;
        foreach (Transform child in players.transform)
        {
            child.gameObject.SetActive(JoinMenuProperties.joinMenuProperties.GetJoinInfo(counter + 1));

            if (child.gameObject.activeSelf == true)
            {
                counter++;
            }
            else
            {
                Destroy(child.gameObject);
            }
        }

        activePlayers = counter;
    }

    private void CountPlayers()
    {
        int counter = 0;
        foreach (Transform child in players.transform)
        {
            if (child.gameObject.activeSelf == true)
            {
                counter++;
            }
        }
        activePlayers = counter;
    }

    private void GetStartPosition()
    {
        int counter = 0;

        foreach (Transform child in players.transform)
        {
            if(child.gameObject != null)
                startingPosition[counter] = child.localPosition;

            counter++;
        }

    }

    private void ResetPlayers()
    {
        int playerNumberIndex;

        foreach (Transform child in players.transform)
        {
            playerNumberIndex = (child.GetComponent<HandController>().PlayerNumber) -1;

            child.localPosition = startingPosition[playerNumberIndex];

            child.gameObject.GetComponent<Collider>().enabled = true;
            child.gameObject.SetActive(true);
        }
    }

}
