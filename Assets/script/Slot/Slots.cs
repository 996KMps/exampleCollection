using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public enum SlotsType
{
    Equipment,
    Inventory,
    Craft,
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

            slots[i].Add(_item);

            switch (_type)
            {
                case SlotsType.Equipment:
                    break;
                case SlotsType.Inventory:
                    invenDel();
                    break;
                case SlotsType.Craft:
                    break;
                case SlotsType.ETC:
                    break;
                default:
                    break;
            }

            return true;
        }

        return false;
    }

    public bool Remove(int _num)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].slotNum != _num)
                continue;

            slots[i].Remove();
            return true;
        }

        return false;
    }

    public bool SlotClear()
    {
        for (int i = 0; i < slots.Length; i++)
            slots[i].Remove();

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
}
