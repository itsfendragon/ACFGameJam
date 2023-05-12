using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Image actorImage;
    public TMP_Text actorName;
    public TMP_Text messageText;
    public RectTransform backgroundBox;
    public GameObject canvas;


    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;
    public static bool isActive = false;

    public float MesssageFlipDelay = 1;
    public float MessageFlipCounter;


    public void OpenDialogue(Message[] messages, Actor[] actors) 
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;
        Debug.Log("Started conversation. Loaded messages: " + messages.Length);
        DisplayMessage();
    }

    void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = messageToDisplay.sprite;

    }

    public void NextMessage()
    {
        activeMessage++;
        MessageFlipCounter = MesssageFlipDelay;
        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            Debug.Log("Conversation ended");
            isActive = false;
            canvas.SetActive(false);
        }
    }

    private void Update()
    {
        //Do not allow the message to change until the user could have read it.
        if (MessageFlipCounter > 0)
            MessageFlipCounter -= Time.deltaTime;
        else if (Input.GetKeyDown(KeyCode.Space) && isActive == true)
        {
            Debug.Log("goinf to next message");
            NextMessage();
        }
    }
}
