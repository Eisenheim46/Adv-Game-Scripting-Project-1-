using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    [SerializeField] private AudioSource sfx;

    [SerializeField] private GameObject controlsImage;

    //private int arenaSceneIndex = 1;

    public void ClickedStartButton()
    {
        StartCoroutine(WaitThenLoadScene(1));
    }

    public void ClickedSoundButton()
    {
        sfx.PlayDelayed(0);
    }

    public void ClickedControlsButton()
    {
        sfx.PlayDelayed(0);

        controlsImage.SetActive(!controlsImage.activeSelf);
    }

    public void ClickedCreditsButton()
    {
        StartCoroutine(WaitThenLoadScene(3));
    }

    public void ClickedQuitButton()
    {
        Application.Quit();
    }

    public void ClickedMenuButton()
    {
        StartCoroutine(WaitThenLoadScene(0));
    }

    private IEnumerator WaitThenLoadScene(int selectedScene)
    {
        sfx.PlayDelayed(0);

        yield return new WaitForSeconds(sfx.clip.length);

        SceneManager.LoadScene(selectedScene);

    }

}
