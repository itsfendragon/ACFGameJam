using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour
{

    public void LoadIntroScript()
    {
        SceneManager.LoadScene("IntroText");

    }

    public void LoadLevelScript() 
    {
        SceneManager.LoadScene("SampleScene");

    }

    public void Quit() 
    {
        Application.Quit();
    }
}
