using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots : MonoBehaviour
{
    public SlotType slotType;
    public string slotName;
    public Slot[] slots;
    public Transform target;

    public void Awake()
    {
        LoadSlots();
    }

    public bool AddItem(Item _item)
    {
        foreach (var slot in slots)
        {
            if (!slot.hasItem)
            {
                slot.Add(_item);
                return true;
            }
        }
        return false;
    }

    public bool RemoveItem(int _num)
    {
        foreach (var slot in slots)
        {
            if (slot.slotNum == _num)
            {
                slot.Remove();
                return true;
            }
        }
        return false;
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
