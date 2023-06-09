using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMount : MonoBehaviour
{

    public float furthestLeft, furthestRight, furthestUp, furthestDown;

    public Transform player;

    public Transform inside;

    public bool indoorsCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!indoorsCam)
        {
            Vector2 newPos = player.position;

            float newx = Mathf.Clamp(newPos.x, furthestLeft, furthestRight);
            float newy = Mathf.Clamp(newPos.y, furthestDown, furthestUp);

            transform.position = new Vector3(newx, newy, 0);
        }
        else 
        {
            transform.position = inside.position;
        }
    }
}
