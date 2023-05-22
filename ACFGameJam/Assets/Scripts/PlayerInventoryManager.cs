using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInventoryManager : MonoBehaviour
{

    //DICTIONARIES ARE NOT SERIALISABLE SO I HAVE TO DO THIS INSTEAD AND COMBINE THEM IN START
    [SerializeField]
    List<string> spriteNames;
    [SerializeField]
    List<Sprite> spriteSprites;
   
    public Dictionary<string, Sprite> spriteBank = new Dictionary<string, Sprite>();

    //Same reason here
    [SerializeField]
    List<string> RecipieString; //seperate by spaces
    [SerializeField]
    List<InventoryItem> result;
    Dictionary<string, InventoryItem> recipies = new Dictionary<string, InventoryItem>();


    [SerializeField]
    Color selectionTint;



    //Dictate When the cookbutton shows up
    public bool CookingPossible;
    [SerializeField]
    Transform Cookbutton;
    [SerializeField]
    Transform CookTrigger;
    [SerializeField]
    float rangeCooking;

    //force it to show up;
    bool cookingDevOverride;

    public bool InventoryOpen;
    [SerializeField]
    Transform inventoryGUI;

    public List<InventoryItem> IngredientSlots = new List<InventoryItem>();
    public bool ShowIngedients = true;
    [SerializeField]
    List<Image> slots = new List<Image>();
    [SerializeField]
    List<TextMeshProUGUI> slotNames = new List<TextMeshProUGUI>();

    public bool ShowDish = true;
    public InventoryItem Dish;
    [SerializeField]
    Image dishSlot;
    [SerializeField]
    TextMeshProUGUI dishName;


    public bool ShowSelected;
    //0, 1, 2, etc, and -1 corresponds to the dish. Out of Range = nothing
    public int SlotSelected;
    [SerializeField]
    TextMeshProUGUI selectedName;
    [SerializeField]
    Image selectedPreview;
    [SerializeField]
    List<Slider> stats = new List<Slider>();

    private void Start()
    {

        //go off the shortest list
        int spritecount = spriteNames.Count;
        if (spriteSprites.Count < spritecount)
            spritecount = spriteSprites.Count;

        //spriteBank.Add("blank", null);

        for (int i = 0; i < spriteNames.Count; i++)
        {
            spriteBank.Add(spriteNames[i], spriteSprites[i]);
        }
        for (int i = 0; i < RecipieString.Count; i++)
        {
            recipies.Add(RecipieString[i], result[i]);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            InventoryOpen = !InventoryOpen;
        }
        CookingPossible = Vector3.Distance(transform.position, CookTrigger.position) < rangeCooking;
        UpdateView();
    }

    //button triggerable script
    public void SelectSlot(int slotIndex) 
    {
        SlotSelected = slotIndex;
    }

    public bool AddIngredient(InventoryItem potentialIngredient) 
    {
        bool nameOverlaps = false;
        int firstFreeSlot = -1;
        for (int i = 0; i < IngredientSlots.Count; i++)
        {
            if (firstFreeSlot == -1 && IngredientSlots[i].exists == false) 
                firstFreeSlot = i;
            if (IngredientSlots[i].Name == potentialIngredient.Name)
                nameOverlaps = true;
        }
        if (nameOverlaps || firstFreeSlot == -1)
            return false;

        IngredientSlots[firstFreeSlot] = potentialIngredient;
        return true;
    }

    public InventoryItem GetIngredient(string name) 
    {
        InventoryItem item = new InventoryItem();

        for (int i = 0; i < IngredientSlots.Count; i++)
        {

            if (IngredientSlots[i].Name == name)
            {
                item = IngredientSlots[i];
                IngredientSlots[i] = new InventoryItem();

            }
        }


        return item;
    }
    public InventoryItem GetIngredient(int position)
    {
        InventoryItem item = new InventoryItem();

        if (IngredientSlots[position].exists)
        {
            item = IngredientSlots[position];
            IngredientSlots[position] = new InventoryItem();
        }    

        return item;
    }

    public void UpdateView() 
    {
        Cookbutton.gameObject.SetActive(CookingPossible || cookingDevOverride);


        inventoryGUI.gameObject.SetActive(InventoryOpen);
        if (inventoryGUI)
        {
            if (ShowIngedients)
            {
                for (int i = 0; i < slots.Count; i++)
                {
                    if (IngredientSlots[i].exists && IngredientSlots[i].Name != "")
                    {
                        slots[i].sprite = spriteBank[IngredientSlots[i].Name];
                        slotNames[i].text = IngredientSlots[i].Name;

                        if (i == SlotSelected)
                            slots[i].color = selectionTint;
                        else
                            slots[i].color = Color.white;
                    }
                    else
                    {
                        slots[i].sprite = spriteBank.ElementAt(0).Value;
                        slotNames[i].text = "";
                    }
                }
            }

            if (Dish.exists && ShowDish) 
            {


                dishSlot.sprite = spriteBank[Dish.Name];
                dishName.text = Dish.Name;

                if (SlotSelected == -1)
                    dishSlot.color = selectionTint;
                else
                    dishSlot.color = Color.white;
            }
            else
            {
                dishSlot.sprite = spriteBank.ElementAt(0).Value;
                dishName.text = "";
            }
        
            if(ShowSelected && SlotSelected < IngredientSlots.Count && SlotSelected > -2)
            {
                if (SlotSelected > -1 && ShowIngedients && IngredientSlots[SlotSelected].exists) 
                {                    
                    selectedPreview.sprite = spriteBank[IngredientSlots[SlotSelected].Name];
                    selectedName.text = IngredientSlots[SlotSelected].Name;

                    if(IngredientSlots[SlotSelected].Type == InventoryItem.InventoryItemType.Ingredient) 
                    {
                        stats[0].value = IngredientSlots[SlotSelected].Sweet;
                        stats[1].value = IngredientSlots[SlotSelected].Savoury;
                        stats[2].value = IngredientSlots[SlotSelected].Harsh;
                        stats[3].value = IngredientSlots[SlotSelected].Meat;
                        stats[4].value = IngredientSlots[SlotSelected].Textured;
                    }
                    else 
                    {
                        for (int i = 0; i < stats.Count; i++)
                        {
                            stats[i].value = 0;
                        }
                    }

                }
                else if (SlotSelected == -1 && ShowDish && Dish.exists) 
                {
                    selectedPreview.sprite = spriteBank[Dish.Name];
                    selectedName.text = Dish.Name;

                    stats[0].value = Dish.Sweet;
                    stats[1].value = Dish.Savoury;
                    stats[2].value = Dish.Harsh;
                    stats[3].value = Dish.Meat;
                    stats[4].value = Dish.Textured;
                }
                else 
                {
                    selectedPreview.sprite = spriteBank.ElementAt(0).Value;
                    selectedName.text = "";

                    for (int i = 0; i < stats.Count; i++)
                    {
                        stats[i].value = 0;
                    }
                }
            }
            else 
            {
                selectedPreview.sprite = spriteBank.ElementAt(0).Value;
                selectedName.text = "";

                for (int i = 0; i < stats.Count; i++)
                {
                    stats[i].value = 0;
                }
            }
        }
        
    }


    public void ClearIngredient(int position)
    {

        if (IngredientSlots[position].exists)
        {
            IngredientSlots[position] = new InventoryItem();
        }
    }

    public void ClearDish()
    {
        Dish = new InventoryItem();
    }

    public void Craft() 
    {
        List<string> gredients = new List<string>();
        gredients.Add(IngredientSlots[0].Name);
        gredients.Add(IngredientSlots[1].Name);
        gredients.Add(IngredientSlots[2].Name);
        int matches = 0;
        string key = "";

        Debug.Log(recipies.Count);

        foreach(KeyValuePair<string, InventoryItem> entry in recipies) 
        {
            matches = 0;
            string[] greds = entry.Key.Split(" ");
            for (int i = 0; i < greds.Length; i++)
            {
                if (gredients.Contains(greds[i]))
                    matches++;
            }

            Debug.Log("recipie matches " + matches + " ingredients");

            if (matches == 3)
            { 
                key = entry.Key;
                break;
            }
        }

        if (key != "")
        {
            Dish = recipies[key];
            for (int i = 0; i < IngredientSlots.Count; i++)
            {
                IngredientSlots[i] = new InventoryItem();
            }
        }


    }
}

[System.Serializable]
public struct InventoryItem 
{
    //Use to make food, is food, is something else someone gave you
    public enum InventoryItemType { Ingredient, Dish, Special }

    public bool exists;
    public string Name;
    public InventoryItemType Type;

    //Let's call these the food groups? Combining together ingredients gets a recipie with a combined balance of these, so it has some indcation of what you'll end up with.
    //Feel free to rename or expand these
    public float Sweet, Savoury, Harsh, Meat, Textured;

}
