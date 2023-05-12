using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayCounter : MonoBehaviour
{
    public TimeSwitch time;

    public TextMeshProUGUI weekday;
    public TextMeshProUGUI daytime;

    string[] daychart = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
    string[] timechart = { "Morning", "Afternoon", "Evening" };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (time.endgame) 
        {
            gameObject.SetActive(false);
            return;
        }

        weekday.text = daychart[time.Day];
        daytime.text = timechart[time.TimeOfDay];

    }
}
