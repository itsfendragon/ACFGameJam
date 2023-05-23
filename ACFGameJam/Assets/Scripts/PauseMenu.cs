using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [Header("Pause Menu")]
    public GameObject panelPause;

    public static bool gameIsPaused;
    public TMP_Text option1;
    public TMP_Text option2;
   // public TMP_Text option3;

    private int numberOfOptions = 3;

    private int selectedOption;

    void Start()
    {
        panelPause.SetActive(false); //pause panel hidden when game starts

        selectedOption = 1;
        option1.color = new Color32(255, 255, 255, 255);
        option2.color = new Color32(0, 0, 0, 255);
      //  option3.color = new Color32(0, 0, 0, 255);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape Key Pressed");

            gameIsPaused = !gameIsPaused;
            PauseGame();
        }

        if (Input.GetKeyDown(KeyCode.S))
        { 
            selectedOption += 1;
            if (selectedOption > numberOfOptions) //If at end of list go back to top
            {
                selectedOption = 1;
            }

            option1.color = new Color32(0, 0, 0, 255); //Make sure all others will be black (or do any visual you want to use to indicate this)
            option2.color = new Color32(0, 0, 0, 255);
         //   option3.color = new Color32(0, 0, 0, 255);

            switch (selectedOption) //Set the visual indicator for which option you are on.
            {
                case 1:
                    option1.color = new Color32(255, 255, 255, 255);
                    break;
                case 2:
                    option2.color = new Color32(255, 255, 255, 255);
                    break;
           //     case 3:
           //         option3.color = new Color32(255, 255, 255, 255);
           //         break;
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        { 
            selectedOption -= 1;
            if (selectedOption < 1) //If at end of list go back to top
            {
                selectedOption = numberOfOptions;
            }

            option1.color = new Color32(0, 0, 0, 255); //Make sure all others will be black (or do any visual you want to use to indicate this)
            option2.color = new Color32(0, 0, 0, 255);
          //  option3.color = new Color32(0, 0, 0, 255);

            switch (selectedOption) //Set the visual indicator for which option you are on.
            {
                case 1:
                    option1.color = new Color32(255, 255, 255, 255);
                    break;
                case 2:
                    option2.color = new Color32(255, 255, 255, 255);
                    break;
           //     case 3:
            //        option3.color = new Color32(255, 255, 255, 255);
            //        break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Picked: " + selectedOption); 

            switch (selectedOption) //Set the visual indicator for which option you are on.
            {
                case 1:
                    UnPause();
                    break;
                case 2:
                    QuitIt();
                    break;
            //    case 3:
            //        /*Do option two*/
             //       break;
            }
        }
    }

    void PauseGame()
    {
        if (gameIsPaused)
        {
            //   firstPersonController.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            AudioListener.pause = true;
            //  canvasCamera.SetActive(false);
            if (panelPause != null)
            {
                panelPause.SetActive(!panelPause.activeSelf);
            }
        }
        else
            UnPause();

    }

    public void UnPause()
    {


        Time.timeScale = 1;
        // firstPersonController.enabled = true;
        AudioListener.pause = false;
        panelPause.SetActive(false);
        Debug.Log("Resume");


    }

    public void QuitIt()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("TitleScreen");

    }
}