using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TimeSwitch : MonoBehaviour
{

    public Transform diaCanvas;

    public Transform homebase;
    public Transform player;

    public int Day;
    public int TimeOfDay;

    int dayperiods = 3;
    int days = 4;

    [SerializeField]
    SpriteRenderer Background;
    [SerializeField]
    SpriteRenderer Buildings;

    public bool endgame;

    //Backgrounds 
    [SerializeField]
    List<Sprite> BGs = new List<Sprite>();
    [SerializeField]
    List<Sprite> BDs = new List<Sprite>();

    public Transform SpawnedNPCs;

    public List<Transform> spawnNPCS = new List<Transform>();

    //This would be best as a dictionary but those aren't visible in editor so FML.
    public List<string> NPCS = new List<string>();
    //If at least one, then that means they're coming to the opening.
    public Dictionary<string, int> NPCFavours = new Dictionary<string, int>();

    //End Card Canvases
    public Transform EndCardGood;
    public Transform EndCardBad;


    bool ProgressTimeDue = false;

    private void Start()
    {
        for (int i = 0; i < NPCS.Count; i++)
        {
            NPCFavours.Add(NPCS[i], 0);
        }
        SpawnedNPCs = Instantiate(spawnNPCS[Day], Vector3.zero, Quaternion.identity) as Transform;

    }

    private void Update()
    {
        if (!diaCanvas.gameObject.activeSelf && ProgressTimeDue) 
        {
            ProgressTime();
        }
    }

    public void RegisterProgressTime()
    {
        ProgressTimeDue = true;

    }
    public void ProgressTime()
    {
        ProgressTimeDue = false;
        //Swap in backgrounds based on time of day, looping around based on how many times there are in the day, 3.
        TimeOfDay++;
        if(TimeOfDay >= dayperiods)
        {
            TimeOfDay = 0;
            Day++;
            Destroy(SpawnedNPCs.gameObject);
            if(Day < days)
                SpawnedNPCs = Instantiate(spawnNPCS[Day], Vector3.zero, Quaternion.identity) as Transform;

        }
        Background.sprite = BGs[TimeOfDay];
        Buildings.sprite = BDs[TimeOfDay];
        //Move around NPCS, change dialogue

        //Allow to cut out the last bit of the last day
        if (Day == days || (Day == days - 1 && TimeOfDay == dayperiods - 1))
        {
            //Move to endgame
            //Load a new scene? but don'tdestroyonload this during that transition, and destroy it again when going back to title screen.
            //Or just put up a title card.

            EndGame();
        }

    }

    public void ProgressTime(int t) 
    {
        for (int i = 1; i < t; i++)
        {
            TimeOfDay++;
        }
        ProgressTime();
    }

    //tally up score, throw up endgame.
    public void EndGame() 
    {
        endgame = true;
        GameObject.Find("Player").SetActive(false);

        int score = 0;
        List<string> visitors = new List<string>();

        foreach (KeyValuePair<string, int> entry in NPCFavours)
        {
            if (entry.Value > 0) 
            { 
                score+= entry.Value;
                visitors.Add(entry.Key);
            }
        }

        if(visitors.Count > 2)
        {
            EndCardGood.gameObject.SetActive(true);
            //List NPCs who visited
            string visitTally = "";
            for (int i = 0; i < visitors.Count -1; i++)
            {
                visitTally += visitors[i] + ", ";
            }
            visitTally += " and " + visitors[visitors.Count - 1] + " came to visit the grand opening, and it looks like they brought their friends along too! \n Score = " + score;
            if (score == visitors.Count * 2)
                visitTally += "\n PERFECT!";
            EndCardGood.GetComponentInChildren<TextMeshProUGUI>().text = visitTally;
        }
        else 
        {
            EndCardBad.gameObject.SetActive(true);
            //List NPCs who visited or none at all;
            string tally = "";
            if (visitors.Count == 2)
                tally = "Only " + visitors[0] + " and " + visitors[1] + " showed up. Not quite enough customers. \n Score = " + score;
            else if (visitors.Count == 1)
                tally = "Only " + visitors[0] + " showed up. You'll need a couple more customers at least. \n Score = " + score;
            else
                tally = "Unfortunately no one showed up this time around. \n Score = " + score; //should only be 0 in this case.

            EndCardBad.GetComponentInChildren<TextMeshProUGUI>().text = tally;

        }

    }

    //press from buttons
    public void ResetGame() 
    {
        //function that returns you to the main menu.

        SceneManager.LoadScene("TitleScreen");
    }
}
