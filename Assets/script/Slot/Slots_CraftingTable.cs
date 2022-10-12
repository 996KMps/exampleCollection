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

    //public bool Add(Item _item)
    //{
    //    Debug.Log(slotName + " Add()");
    //    for (int i = 0; i < slots.Length; i++)
    //    {
    //        if (!slots[i].hasItem)
    //        {
    //            slots[i].Add(_item);
    //            return true;
    //        }
    //    }
    //    return false;
    //}

    public bool Crafting(int _num)
    {
        Debug.Log(slotName + " Crafting()");
        if (slots[_num].hasItem)
        {
            foreach (int item in slots[_num].item.connectingNeedItemSlot)
                inven.slots[item].Remove();

            inven.AddItem(slots[_num].item);
            slots[_num].Remove();

            return true;
        }
        
        return false;
    }
    public bool Clear()
    {
        Debug.Log(slotName + " Clear()");
        foreach (Slot slot in slots)
        {
            slot.Remove();
        }
        return true;
    }
}
