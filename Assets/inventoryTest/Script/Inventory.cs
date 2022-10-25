using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int slotCnt;
    public List<Item> items = new List<Item>();
    public Slot[] slots;
    public Transform slotHolder;

    private void Start()
    {
        slotCnt = 10;
        slots = slotHolder.GetComponentsInChildren<Slot>();

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotNum = i;
        }
    }

    public bool AddItem(Item _item)
    {
        bool isAdd = false;
        if (items.Count < slotCnt)
        {
            items.Add(_item);
            return true;
        }
        return isAdd;
    }

    public bool DestroyItem(Item _item)
    {
        bool isDetsroyed = false;
        if (true)
        {

        }
        return isDetsroyed;
    }
}