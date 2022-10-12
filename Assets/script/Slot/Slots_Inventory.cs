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

        //상위 아이템 목록을 담아놓을 리스트
        List<string> list = new();

        //인벤토리 안 아이템들의 상위아이템 이름을 리스트에 넣기
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item.hightTierItems != null)
            {
                for (int j = 0; j < slots[i].item.hightTierItems.Length; j++)
                {
                    for (int k = 0; k < ItemDataBase.instance.itemDB.Count; k++)
                    {
                        if (ItemDataBase.instance.itemDB[k].name == slots[i].item.hightTierItems[j])
                        {
                            list.Add(ItemDataBase.instance.itemDB[k].name);
                        }
                    }
                }
            }
        }

        Debug.Log("인벤토리 안 아이템들의 상위아이템 이름을 리스트에 넣기 완료");

        //중복 걸러내기
        List<string> distlist;
        distlist = list.Distinct().ToList();

        Debug.Log("중복된 이름 걸러내기 완료");

        //걸러낸 리스트를 이용해서 상위 아이템의 정보들을 담기
        List<Item> highTierItems = new();

        for (int i = 0; i < distlist.Count; i++)
        {
            for (int j = 0; j < ItemDataBase.instance.itemDB.Count; j++)
            {
                if (ItemDataBase.instance.itemDB[j].name == distlist[i])
                {
                    highTierItems.Add(ItemDataBase.instance.itemDB[j]);
                }
            }
        }

        Debug.Log("걸러낸 리스트를 이용해서 상위 아이템의 정보들을 담기 완료");

        //재료를 충족할 시에 리스트에 넣어줌
        List<Item> distHighTier = new();

        for (int i = 0; i < highTierItems.Count; i++)
        {
            List<bool> isFills = new();
            highTierItems[i].connectingNeedItemSlot.Clear();

            for (int j = 0; j < highTierItems[i].needItems.Length; j++)
            {
                bool isFill = false;

                for (int k = 0; k < slots.Length; k++)
                {
                    if (highTierItems[i].needItems[j] == slots[k].item.name)
                    {
                        highTierItems[i].connectingNeedItemSlot.Add(slots[k].slotNum);
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
                distHighTier.Add(highTierItems[i]);
            }
        }

        Debug.Log("충족한 아이템 리스트에 담기 완료");

        //넣기 전에 초기화 한번
        craftingTable.Clear();
        //넣기
        for (int i = 0; i < distHighTier.Count; i++)
        {
            craftingTable.AddItem(distHighTier[i]);
        }

        Debug.Log("넣기 전에 초기화 한번, 넣기 완료");

        return true;
    }
}
