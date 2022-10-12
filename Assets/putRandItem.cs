using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class putRandItem : MonoBehaviour
{
    public Slots_Inventory inven;
    public Slots_CraftingTable craft;
    public void clickAdd()
    {
        inven.AddItem(ItemDataBase.instance.itemDB[Random.Range(0, 2)]);
    }
    public void clickCraft()
    {
        craft.Crafting(0);
    }
}
