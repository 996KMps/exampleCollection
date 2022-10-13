using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots_CraftingTable : Slots
{
    public Slots_Inventory inven;

    private void Start()
    {
        slotType = SlotsType.CraftingTable;
    }

    public bool Crafting(int _num)
    {
        Debug.Log(slotName + " Crafting()");
        if (slots[_num].hasItem)
        {
            foreach (int item in slots[_num].item.connectingNeedItemSlot)
                inven.slots[item].RemoveItem();

            inven.Add(slots[_num].item, inven.slotType);
            slots[_num].RemoveItem();

            return true;
        }

        return false;
    }
}