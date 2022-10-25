using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots_EquipmentWindow : Slots
{
    public Slots_Inventory inven;
    public bool hasWeapon;
    public bool hasProtector;

    private void Start()
    {
        slotType = SlotsType.Equipment;
    }

    public bool EquipWeapon(Item _Item)
    {
        Debug.Log(slotName + " EquipWeapon()");
        if (!hasWeapon)
        {
            slots[0].item = _Item;
            return true;
        }
        if (hasWeapon)
        {
            Item tempItem = slots[0].item;
            slots[0].item = _Item;
            inven.Add(tempItem, inven.slotType);
            return true;
        }
        return false;
    }
    public bool EquipProtector(Item _Item)
    {
        Debug.Log(slotName + " EquipProtector()");
        if (!hasProtector)
        {
            slots[1].item = _Item;
            return true;
        }
        if (hasProtector)
        {
            Item tempItem = slots[1].item;
            slots[1].item = _Item;
            inven.Add(tempItem, inven.slotType);
            return true;
        }
        return false;
    }
}
