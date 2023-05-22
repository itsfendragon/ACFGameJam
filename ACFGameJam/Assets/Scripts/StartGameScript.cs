using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour
{
    //Expected this to be used once, deprecated in favour of LoadLevel
    public void LoadIntroScript()
    {
        SceneManager.LoadScene("IntroText");

    }

    public void LoadLevelScript() 
    {
        SceneManager.LoadScene("SampleScene");

    }


    public void LoadTitleScript()
    {
        SceneManager.LoadScene("TitleScreen");

    }

    public void LoadCreditsScript()
    {
        SceneManager.LoadScene("Credits");
        
    }

    public void LoadLevel(string level) 
    {
        SceneManager.LoadScene(level);

    }

    public void Quit() 
    {
        Application.Quit();
    }
}
