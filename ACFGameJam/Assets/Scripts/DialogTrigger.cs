using UnityEngine;
using System.Collections.Generic;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField]
    PlayerInventoryManager pim;

    public TimeSwitch time;

    [SerializeField]
    List<string> acceptableDishes = new List<string>();

    public int SpriteChar = 0;

    public Message[] message;
    public Message[] idle;
    public Message[] failure;
    public Message[] success;
    public Actor[] actors;

    public bool Hello;
    public bool Exhausted = false;

    public void Start()
    {
        GetComponent<Animator>().SetInteger("Character", SpriteChar);
        time = GameObject.Find("Timeswitch").GetComponent<TimeSwitch>();
    }

    public void StartDialogue()
    {
        pim = GameObject.Find("Player").GetComponent<PlayerInventoryManager>();
        if (Exhausted)
            return;
        //Need something that says if first time talking or something
        if (Hello)
        {
            Hello = false;
            FindObjectOfType<DialogManager>().OpenDialogue(message, actors);
        }
        else if (pim.Dish.exists)
        {
            //if the right dish is in the inventory
            if (acceptableDishes.Contains(pim.Dish.Name))
            {
                Exhausted = true;
                pim.Dish = new InventoryItem();
                Debug.Log("timenames " + time.NPCFavours.Count);
                time.NPCFavours[actors[1].name]++;
                FindObjectOfType<DialogManager>().OpenDialogue(success, actors);
                time.RegisterProgressTime();
            }
            //if the wrong dish is in the inventory
            else
            {
                Exhausted = true;
                pim.Dish = new InventoryItem();
                FindObjectOfType<DialogManager>().OpenDialogue(failure, actors);
                time.RegisterProgressTime();
            } 
        }
        else
            FindObjectOfType<DialogManager>().OpenDialogue(idle, actors);
    }
}

[System.Serializable]
public class Message
{
    public int actorId;
    public string message;
    public Sprite sprite;
}

public class Failure
{
    public int actorId;
    public string failure;
    public Sprite sprite;

}

public class Success
{
    public int actorId;
    public string success;
    public Sprite sprite;
}


[System.Serializable]
public class Actor
{
    public string name;
    //public Sprite sprite;
}
