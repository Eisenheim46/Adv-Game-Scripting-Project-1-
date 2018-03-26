using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinMenuProperties : MonoBehaviour {

    public static JoinMenuProperties joinMenuProperties;

    private static bool[] hasPlayerJoined = new bool[4];

    private void Awake()
    {
        if (joinMenuProperties != null && joinMenuProperties != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            joinMenuProperties = this;
        }
    }

    private void Start()
    {
       // Debug.Log(hasPlayerJoined[0]);
    }

    public bool GetJoinInfo (int playerNumber)
    {
        int playerNumberIndex = playerNumber - 1;

        return hasPlayerJoined[playerNumberIndex];
    }

    public bool SetJoinInfo (int playerNumber, bool isJoined)
    {
        int playerNumberIndex = playerNumber - 1;

        hasPlayerJoined[playerNumberIndex] = isJoined;

        return hasPlayerJoined[playerNumberIndex];
    }

}
