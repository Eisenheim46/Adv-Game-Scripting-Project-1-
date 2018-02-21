using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private int arenaSceneIndex = 1;

    public void ClickedStartButton()
    {
        SceneManager.LoadScene(arenaSceneIndex);
    }

    public void ClickedControlsButton()
    {

    }

    public void ClickedQuitButton()
    {
        Application.Quit();
    }

}
