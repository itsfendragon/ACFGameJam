using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneWithKey : MonoBehaviour
{
    public KeyCode sceneLoadButton;
    public string sceneToLoad;
    bool hasTriggered;


    // spaghetti code that I've carried with me from jam to jam lol
    void Update()
    {
        if (Input.GetKeyDown(sceneLoadButton) && hasTriggered == false)
        {
            SceneManager.LoadScene(sceneToLoad);
            hasTriggered = true;
            //the hasTriggered flag is more important here because we want to make sure not to call this process and slow down the game
            //multiple times if the player presses the button a few times accidentally
        }
    }
}
