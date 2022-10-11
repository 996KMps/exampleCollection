using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots_CraftingTable : Slots
{
    public Slots_Inventory inven;

    private void Start()
    {
        slotType = SlotType.Crafting;
    }

    public bool Add(Item _item)
    {
        Debug.Log(slotName + " Add()");
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].hasItem)
            {
                slots[i].Change(_item);
                return true;
            }
        }
        return false;
    }
    public bool Crafting(int _num)
    {
        Debug.Log(slotName + " Crafting()");
        if (slots[_num].hasItem)
        {
            foreach (var item in slots[_num].item.connectingNeedItem)
                item.Destroy();

            inven.Add(slots[_num].item);
            slots[_num].Change(new Item());
            return true;
        }
        
        return false;
    }
    public bool Clear()
    {
        Debug.Log(slotName + " Clear()");
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].item.Destroy();
        }
        return true;
    }
}
