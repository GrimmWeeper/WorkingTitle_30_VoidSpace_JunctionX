using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//0:Menu 1:Login 2:About 3:AR 4:Setting

public class MainMenu : MonoBehaviour
{
    public void ToLogin()
    {
        SceneManager.LoadScene(1);
    }

    public void ToAbout()
    {
        SceneManager.LoadScene(2);
    }

    public void ToSetting()
    {
        SceneManager.LoadScene(4);
    }

    public void ToGame()
    {
        SceneManager.LoadScene(3);
    }

    public void BackMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
