using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class Slots_Inventory : Slots
{
    public Slots_EquipmentWindow equipmentSlot;
    public Slots_CraftingTable craftingTable;

    private void Start()
    {
        slotType = SlotType.Inventory;
    }

    public bool AddItem(Item _item)
    {
        foreach (var slot in slots)
        {
            if (!slot.hasItem)
            {
                slot.Add(_item);
                CheckHighTier();
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
                CheckHighTier();
                return true;
            }
        }
        return false;
    }

    public bool RemoveAll()
    {
        Debug.Log(slotName + " RemoveAll()");
        foreach (Slot slot in slots)
        {
            slot.Remove();
        }
        return true;
    }
    public bool Swap(int _num1, int _num2)
    {
        Debug.Log(slotName + " Swap()");
        Item tempItem;

        if (slots[_num1].item == null)
            tempItem = new Item();
        else
            tempItem = slots[_num1].item;

        slots[_num1].Add(slots[_num2].item);
        slots[_num2].Add(tempItem);
        return true;
    }
    public bool ItemUse(int _slotNum)
    {
        Debug.Log(slotName + " ItemUse()");
        switch (slots[_slotNum].item.type)
        {
            case ItemType.Weapon:
                equipmentSlot.EquipWeapon(slots[_slotNum].item);
                break;
            case ItemType.Protector:
                equipmentSlot.EquipProtector(slots[_slotNum].item);
                break;
            case ItemType.Consumable:
                break;
            case ItemType.ETC:
                break;
            case ItemType.NULL:
                break;
            default:
                break;
        }
        return false;
    }
    public bool CheckHighTier()
    {
        Debug.Log(slotName + " CheckHighTier()");

        //���ۿ��� ������� �κ��丮 �� ��� �������� ���� �������� ��Ƴ��� ����Ʈ
        List<string> itemList = new();
        //list���� �ߺ��� ��Ҹ� ������ ����Ʈ
        List<string> distList;
        //distlist�� ItemDB���� �������� ã�� ���� ����Ʈ
        List<Item> ItemInfo = new();
        //���۰����� ������ ������ �־��� ����Ʈ
        List<Item> craftableList = new();

        //list�� ���������� �̸� �ֱ�
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item.hightTierItems == null)
                continue;
            
            for (int j = 0; j < slots[i].item.hightTierItems.Length; j++)
            {
                for (int k = 0; k < ItemDataBase.instance.itemDB.Count; k++)
                {
                    if (ItemDataBase.instance.itemDB[k].name == slots[i].item.hightTierItems[j])
                    {
                        itemList.Add(ItemDataBase.instance.itemDB[k].name);
                    }
                }
            }
        }

        //�ߺ� �ɷ�����
        distList = itemList.Distinct().ToList();

        //highTierItems�� ������ ���� ã�Ƽ� �ֱ�
        for (int i = 0; i < distList.Count; i++)
        {
            for (int j = 0; j < ItemDataBase.instance.itemDB.Count; j++)
            {
                if (ItemDataBase.instance.itemDB[j].name == distList[i])
                {
                    ItemInfo.Add(ItemDataBase.instance.itemDB[j]);
                    break;
                }
            }
        }

        //distHighTier
        for (int i = 0; i < ItemInfo.Count; i++)
        {
            List<bool> isFills = new();
            ItemInfo[i].connectingNeedItemSlot.Clear();

            for (int j = 0; j < ItemInfo[i].needItems.Length; j++)
            {
                bool isFill = false;

                for (int k = 0; k < slots.Length; k++)
                {
                    if (ItemInfo[i].needItems[j] == slots[k].item.name)
                    {
                        ItemInfo[i].connectingNeedItemSlot.Add(slots[k].slotNum);
                        isFill = true;
                        break;
                    }
                }

                isFills.Add(isFill);
            }

            bool fillCheck = true;

            for (int j = 0; j < isFills.Count; j++)
            {
                fillCheck = fillCheck && isFills[j];
            }

            if (fillCheck)
            {
                craftableList.Add(ItemInfo[i]);
            }
        }

        Debug.Log("������ ������ ����Ʈ�� ��� �Ϸ�");

        //�ֱ� ���� �ʱ�ȭ �ѹ�
        craftingTable.Clear();

        //�ֱ�
        for (int i = 0; i < craftableList.Count; i++)
        {
            craftingTable.AddItem(craftableList[i]);
        }

        Debug.Log("�ֱ� ���� �ʱ�ȭ �ѹ�, �ֱ� �Ϸ�");

        return true;
    }
}
