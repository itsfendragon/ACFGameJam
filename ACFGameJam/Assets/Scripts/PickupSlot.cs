using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSlot : MonoBehaviour
{

    
    public InventoryItem Item;
    public int AmountStocked;

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


        //Handles display and pickup in the script
        //Make sure the marker is sorted in the layer to render above everything else
        if(Vector3.Distance(pim.transform.position, transform.position) <= pickupRange)
        {
            pickupMarker.gameObject.SetActive(true);


            if (AmountStocked > 0 && Input.GetKeyDown(KeyCode.E)) 
            {
                if (Item.Type != InventoryItem.InventoryItemType.Dish)
                    if(pim.AddIngredient(Item))
                        AmountStocked--;

            }
        }
        else 
        {
            pickupMarker.gameObject.SetActive(false);
        }

        if (AmountStocked <= 0)
            gameObject.SetActive(false);
    }
}
