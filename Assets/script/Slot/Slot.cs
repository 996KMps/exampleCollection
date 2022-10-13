using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Slot : MonoBehaviour
{
    public int slotNum;
    public bool hasItem;
    public Item item;
    public Image icon;
    public TextMeshProUGUI amount;

    public bool Add(Item _item)
    {
        item = _item;
        item.amount++;
        hasItem = true;
        SlotDataUpdate();
        return true;
    }

    public bool Remove()
    {
        item = new();
        SlotDataUpdate();
        return true;
    }

    public bool SlotDataUpdate()
    {
        if (!hasItem)
            return false;
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
