using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GomDolInventoryUI : MonoBehaviour
{
    GomDolInventory inventory;

    public GameObject inventoryPanel;
    bool activeInventory = false;

    public GomDolSlot[] slots;
    public Transform slotHolder;

    private void Start()
    {
        inventory = GomDolInventory.instance;
        slots = slotHolder.GetComponentsInChildren<GomDolSlot>();
        inventory.onSlotCountChange += SlotChange;
        inventoryPanel.SetActive(activeInventory);
    }

    private void SlotChange(int val)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.SlotCnt)
                slots[i].GetComponent<Button>().interactable = true;
            else
                slots[i].GetComponent<Button>().interactable = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
    }
    public void AddSlot()
    {
        inventory.SlotCnt++;
    }
}
