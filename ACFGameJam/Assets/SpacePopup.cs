using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacePopup : MonoBehaviour
{
    //code shamelessly stolen from the pickup code to save on thinking.


    [SerializeField]
    float pickupRange;

    PlayerInventoryManager pim;

    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        pim = GameObject.Find("Player").GetComponent<PlayerInventoryManager>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //quickly just set this. no if needed
        sr.enabled = Vector3.Distance(pim.transform.position, transform.position) <= pickupRange;

    }
}
