using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Canvas AboutCanvas;
    public Canvas ControlsCanvas;
    public Canvas MainMenuCanvas;
    public void PlayGame()
    {
        SceneManager.LoadScene("Level01");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void Controls()
    {
        MainMenuCanvas.enabled = false;
        ControlsCanvas.enabled = true;
    }

    public void About()
    {
        MainMenuCanvas.enabled = false;
        AboutCanvas.enabled = true;
    }

    public void Back()
    {  
        MainMenuCanvas.enabled = true;
        AboutCanvas.enabled = false;
        ControlsCanvas.enabled = false;
    }

}
