using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Caroselnventory : MonoBehaviour
{
    [SerializeField]
    PlayerInventoryManager pim;

    [SerializeField]
    Image cycleBox;

    public float IconFlipDelay = 2;
    public float IconFlipCounter;

    int currentIcon;

    private void Update()
    {
        //Do not allow the message to change until the user could have read it.
        if (IconFlipCounter > 0)
            IconFlipCounter -= Time.deltaTime;
        else
        {
            IconFlipCounter = IconFlipDelay;
            currentIcon++;
            if (currentIcon > 3)
                currentIcon = 0;
            cycleBox.sprite = pim.spriteBank["nothing"];
            //Cycle through by mod so it can fall through to the top
            for (int i = currentIcon + 4; i >= 0; i--)
            {
                if (i % 4 == pim.IngredientSlots.Count)
                {
                    if (pim.Dish.exists)
                    {
                        cycleBox.sprite = pim.spriteBank[pim.Dish.Name];
                        break;
                    }
                    else
                        continue;
                }


                if (pim.IngredientSlots[i % 4].exists) 
                { 
                    cycleBox.sprite = pim.spriteBank[pim.IngredientSlots[i % 4].Name];
                    break;
                }
            }
        }
    }
}
