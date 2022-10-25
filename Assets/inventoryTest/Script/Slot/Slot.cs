using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;


[System.Serializable]
public class Slot : MonoBehaviour
{
    public int slotNum;
    public bool hasItem;
    public Item item;
    public Image icon;
    public TextMeshProUGUI amount;

    public bool AddItem(Item _item)
    {
        hasItem = true;
        item = _item;
        SlotDataUpdate();
        return true;
    }

    public bool RemoveItem()
    {
        hasItem = false;
        item = null;
        SlotDataUpdate();
        return true;
    }

    public bool SlotDataUpdate()
    {
        if (item == null)
        {
            icon.sprite = null;
            amount.text = null;
            return true;
        }

        icon.sprite = item.img;
        amount.text = item.amount.ToString();

        switch (item.type)  
        {

            case ItemType.Weapon:
                
                return true;

            case ItemType.Protector:
                return true;

            case ItemType.Consumable:
                return true;

            case ItemType.ETC:
                return true;

            case ItemType.NULL:
                hasItem = false;
                return true;

            default:
                return false;
        }
    }
}
