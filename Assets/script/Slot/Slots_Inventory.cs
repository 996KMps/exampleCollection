using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        //���� ������ ����� ��Ƴ��� ����Ʈ
        List<string> list = new();

        //�κ��丮 �� �����۵��� ���������� �̸��� ����Ʈ�� �ֱ�
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item.hightTierItems != null)
            {
                for (int j = 0; j < slots[i].item.hightTierItems.Length; j++)
                {
                    foreach (Item item in ItemDataBase.instance.itemDB)
                    {
                        if (item.name == slots[i].item.hightTierItems[j])
                        {
                            list.Add(item.name);
                        }
                    }
                }
            }
        }
        //��� üũ
        foreach (var item in list)
        {
            Debug.Log("first sort : " + item);
        }

        Debug.Log("�κ��丮 �� �����۵��� ���������� �̸��� ����Ʈ�� �ֱ� �Ϸ�");

        //�ߺ� �ɷ�����
        List<string> distlist;
        distlist = list.Distinct().ToList();

        Debug.Log("�ߺ��� �̸� �ɷ����� �Ϸ�");

        //��� üũ
        foreach (var item in distlist)
        {
            Debug.Log("second sort : " + item);
        }

        //�ɷ��� ����Ʈ�� �̿��ؼ� ���� �������� �������� ���
        List<Item> highTierItems = new();

        foreach (string item in distlist)
        {
            foreach (var dbItem in ItemDataBase.instance.itemDB)
            {
                if (dbItem.name == item)
                {
                    highTierItems.Add(dbItem);
                }
            }
        }

        //üũ
        foreach (var item in highTierItems)
        {
            Debug.Log("Third sort : " + item.name);
        }

        Debug.Log("�ɷ��� ����Ʈ�� �̿��ؼ� ���� �������� �������� ��� �Ϸ�");

        //��Ḧ ������ �ÿ� ����Ʈ�� �־���
        List<Item> distHighTier = new();

        foreach (Item item in highTierItems)
        {
            item.connectingNeedItemSlot.Clear();
            List<bool> isFills = new();
            isFills.Clear();

            for (int i = 0; i < item.needItems.Length; i++)
            {
                Debug.Log("needItem.Length : " + item.needItems.Length + ", loop " + (i + 1));
                bool isFill = false;

                for (int j = 0; j < slots.Length; j++)
                {
                    Debug.Log("slots.Length: " + slots.Length + ", loop " + (j + 1));
                    //Debug.Log("equal? : " + item.needItems[i] + " == " + slots[j].item.name);
                    if (item.needItems[i] == slots[j].item.name)
                    {
                        Debug.Log("equal? : " + item.needItems[i] + " == " + slots[j].item.name);
                        item.connectingNeedItemSlot.Add(slots[j].slotNum);
                        isFill = true;
                        break;
                    }
                }
                //foreach (Slot invenItem in slots)
                //{
                //    Debug.Log("equal? : " + item.needItems[i] + " == " + invenItem.item.name);
                //    if (item.needItems[i] == invenItem.item.name)
                //    {
                //        item.connectingNeedItemSlot.Add(invenItem.slotNum);
                //        isFill = true;
                //        break;
                //    }
                //}

                isFills.Add(isFill);
            }

            foreach (var items in highTierItems)
            {
                Debug.Log("len" + items.connectingNeedItemSlot.Count);
            }

            bool fillCheck = true;

            foreach (var _fill in isFills)
            {
                Debug.Log("fillCheck : " + fillCheck);
                fillCheck = fillCheck&&_fill;
            }

            Debug.Log("fillCheck : " + fillCheck);

            if (fillCheck)
            {
                distHighTier.Add(item);
            }
        }

        Debug.Log("������ ������ ����Ʈ�� ��� �Ϸ�");

        //��� Ȯ��
        foreach (var item in distHighTier)
        {
            Debug.Log("fourth sort : " + item.name);
        }

        //�ֱ� ���� �ʱ�ȭ �ѹ�
        craftingTable.Clear();
        //�ֱ�
        foreach (var item in distHighTier)
        {
            craftingTable.AddItem(item);
        }

        Debug.Log("�ֱ� ���� �ʱ�ȭ �ѹ�, �ֱ� �Ϸ�");

        return true;
    }
}
