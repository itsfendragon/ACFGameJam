using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCook : MonoBehaviour
{

    [SerializeField]
    float pickupRange;
    //E in a box
    [SerializeField]
    Transform pickupMarker;

    //reference to player, make sure to set this
    [SerializeField]
    PlayerInventoryManager pim;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(pim.transform.position, transform.position) <= pickupRange)
        {
            pickupMarker.gameObject.SetActive(true);


            if (Input.GetKeyDown(KeyCode.E))
            {
                pim.CookingPossible = true;
                pim.InventoryOpen = true;
            }
        }
        else
        {
            pickupMarker.gameObject.SetActive(false);
        }

    }
}
