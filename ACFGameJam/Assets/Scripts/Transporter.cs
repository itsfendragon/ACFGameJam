using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transporter : MonoBehaviour
{

    [SerializeField]
    float pickupRange;
    //E in a box
    [SerializeField]
    Transform pickupMarker;

    //reference to player, make sure to set this
    [SerializeField]
    PlayerMovement pm;

    [SerializeField]
    Transform Output;

    [SerializeField]
    CameraMount cm;
    [SerializeField]
    bool setInside;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (pm.transportCooldown <= 0 && Vector3.Distance(pm.transform.position, transform.position) <= pickupRange)
        {
            pickupMarker.gameObject.SetActive(true);


            if (Input.GetKeyDown(KeyCode.E))
            {
                cm.indoorsCam = setInside;
                pm.transportCooldown = 1;
                pm.transform.position = Output.position;
            }
        }
        else
        {
            pickupMarker.gameObject.SetActive(false);
        }

    }
}
