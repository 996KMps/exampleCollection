using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum SlotType
{
    Equipment,
    Inventory,
    Crafting,
    ETC,
}
[System.Serializable]
public class Slot : MonoBehaviour
{
    public int slotNum;
    public bool hasItem;
    public Item item;
    public Image icon;
    public TextMeshProUGUI amount;

    private void Update()
    {
    }

    public bool Change(Item _item)
    {
        Debug.Log("SlotNum " + slotNum + " : Change()");
        item = _item;
        hasItem = true;
        SlotDataUpdate();
        return true;
    }

    public bool SlotDataUpdate()
    {
        if (hasItem)
        {
            Debug.Log("SlotNum " + slotNum + " : SlotDataUpdate()");
            switch (item.type)  
            {
                case ItemType.Weapon:
                    icon.sprite = item.img;
                    amount.text = item.amount.ToString();
                    break;
                case ItemType.Protector:
                    icon.sprite = item.img;
                    amount.text = item.amount.ToString();
                    break;
                case ItemType.Consumable:
                    icon.sprite = item.img;
                    amount.text = item.amount.ToString();
                    break;
                case ItemType.ETC:
                    icon.sprite = item.img;
                    amount.text = item.amount.ToString();
                    break;
                case ItemType.NULL:
                    hasItem = false;
                    break;
                default:
                    break;
            }
        }
        return false;
    }
}
