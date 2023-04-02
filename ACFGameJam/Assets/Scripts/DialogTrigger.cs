using UnityEditor.Compilation;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Message[] message;
    public Message[] failure;
    public Message[] success;
    public Actor[] actors;

    public void StartDialogue()
    {
        //Need something that says if first time talking or something
        {
            FindObjectOfType<DialogManager>().OpenDialogue(message, actors);
        }
        //if the wrong dish is in the inventory
        {
            FindObjectOfType<DialogManager>().OpenDialogue(failure, actors);
        }
        //if the right dish is in the inventory
        {
            FindObjectOfType<DialogManager>().OpenDialogue(success, actors);
        }
    }
}

[System.Serializable]
public class Message
{
    public int actorId;
    public string message;
}

public class Failure
{
    public int actorId;
    public string failure;
}

public class Success
{
    public int actorId;
    public string success;
}


[System.Serializable]
public class Actor
{
    public string name;
    public Sprite sprite;
}
