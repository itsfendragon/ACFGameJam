using UnityEngine;
using UnityEngine.UI;

public class NPCTalk : MonoBehaviour
{

    public DialogTrigger trigger;
    public bool isInRange;
    public GameObject canvas;


    private void Start()
    {
        canvas.SetActive(false); //canvas hidden when game starts

    }

    private void Update()
    {
        if (isInRange) //when player is near the npc
        {
            if (canvas != null) //if the dialogue box is hidden 
            {
                if (Input.GetKeyDown(KeyCode.Space)) //and the press the space bar 
                {
                    isInRange = false;
                    canvas.SetActive(true); //the dialogue box will appear and the conversation will start
                    trigger.StartDialogue();
                }
            
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) //determines if the player is in range of the npc
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            Debug.Log("Player now in range");
        }
    }

    void OnTriggerExit2D(Collider2D other) //determines when the player is out of range of the npc
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            Debug.Log("Player now out of range");
        }
    }
}
