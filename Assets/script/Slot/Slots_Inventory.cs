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
    public bool Add(Item _item)
    {
        Debug.Log(slotName + " Add()");
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].hasItem)
            {
                slots[i].Change(_item);
                CheckHighTier();
                return true;
            }
        }
        return false;
    }
    public bool Remove(int _num)
    {
        Debug.Log(slotName + " Remove()");
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].slotNum == _num)
            {
                slots[i].item.Destroy();
                return true;
            }
        }
        return false;
    }
    public bool RemoveAll()
    {
        Debug.Log(slotName + " RemoveAll()");
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].item.Destroy();
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

        slots[_num1].Change(slots[_num2].item);
        slots[_num2].Change(tempItem);
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
        List<string> list = new List<string>();

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

        //�ߺ��� �̸� �ɷ�����
        List<string> distlist = new List<string>();
        distlist = list.Distinct().ToList();

        Debug.Log("�ߺ��� �̸� �ɷ����� �Ϸ�");

        //��� üũ
        foreach (var item in distlist)
        {
            Debug.Log("second sort : " + item);
        }

        //�ɷ��� ����Ʈ�� �̿��ؼ� ���� �������� �������� ���
        List<Item> highTierItems = new List<Item>();

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
        List<Item> distHighTier = new List<Item>();

        foreach (Item item in highTierItems)
        {
            List<bool> isFills = new List<bool>();

            for (int i = 0; i < item.needItems.Length; i++)
            {
                bool isFill = false;

                foreach (var invenItem in slots)
                {
                    Debug.Log("equal? : " + item.needItems[i] + " == " + invenItem.item.name);
                    if (item.needItems[i] == invenItem.item.name)
                    {
                        item.connectingNeedItem.Add(invenItem.item);
                        isFill = true;
                        break;
                    }
                }

                isFills.Add(isFill);
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

        foreach (var item in distHighTier)
        {
            Debug.Log("fourth sort : " + item.name);
        }

        //�ֱ� ���� �ʱ�ȭ �ѹ�
        craftingTable.Clear();
        //�ֱ�
        foreach (var item in distHighTier)
        {
            craftingTable.Add(item);
        }

        Debug.Log("�ֱ� ���� �ʱ�ȭ �ѹ�, �ֱ� �Ϸ�");

        return true;
    }
}
