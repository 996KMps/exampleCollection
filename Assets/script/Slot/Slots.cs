using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public enum SlotsType
{
    Equipment,
    Inventory,
    CraftingTable,
    ETC,
}

public class Slots : MonoBehaviour
{
    public SlotsType slotType;
    public string slotName;
    public Slot[] slots;
    public Transform target;

    public delegate bool Del();
    public Del invenDel;

    public void Awake()
    {
        LoadSlots();
    }

    public bool Add(Item _item, SlotsType _type)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].hasItem)
                continue;

            slots[i].AddItem(_item);

            OnDelForSlotsType(_type);

            return true;
        }

        return false;
    }

    public bool Remove(int _num, SlotsType _type)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].slotNum != _num)
                continue;

            slots[i].RemoveItem();

            OnDelForSlotsType(_type);

            return true;
        }

        return false;
    }

    public bool SlotClear()
    {
        for (int i = 0; i < slots.Length; i++)
            slots[i].RemoveItem();

        return true;
    }
    public bool LoadSlots()
    {
        Debug.Log(slotName + " LoadSlots()");
        slots = target.GetComponentsInChildren<Slot>();
        if (slots != null)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].slotNum = i;
            }
            return true;
        }
        return false;
    }
    public bool OnDelForSlotsType(SlotsType _type)
    {
        switch (_type)
        {
            case SlotsType.Equipment:
                return true;
            case SlotsType.Inventory:
                invenDel();
                return true;
            case SlotsType.CraftingTable:
                return true;
            case SlotsType.ETC:
                return true;
            default:
                break;
        }

        return false;
    }
}
