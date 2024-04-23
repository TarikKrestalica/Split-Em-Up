using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] GameObject title;
    [SerializeField] GameObject tutorial;

    public void GoToTutorial()
    {
        tutorial.SetActive(true);
        title.SetActive(false);
    }

    public void ExitTutorial()
    {
        title.SetActive(true);
        tutorial.SetActive(false);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("Game");
    }
}
