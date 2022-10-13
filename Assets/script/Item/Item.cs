using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Protector,
    Consumable,
    ETC,
    NULL,
}

[System.Serializable]
public class Item
{
    //Ÿ��
    public ItemType type = ItemType.NULL;

    //�̸�
    public string name;

    //�̹���
    public Sprite img;

    //ȿ��
    public List<ItemEft> efts;

    //����
    public int amount;

    //����������
    public string[] hightTierItems;

    //�� �������� ����µ� �ʿ������
    public string[] needItems;

    //�Ҹ��� ��� ����
    public List<int> connectingNeedItemSlot = new();

    //���
    public bool Use()
    {
        Debug.Log(name + " : Use()");
        bool isUsed = false;
        foreach (ItemEft eft in efts)
        {
            isUsed = eft.ExcuteRole();
        }
        return isUsed;
    }
    //������
    public bool Throw()
    {
        Debug.Log(name + " : Throw()");
        return false;
    }
    //������
    public bool Dump()
    {
        if (amount > 1)
        {
            amount--;
            //DUMP TODO
            return true;
        }
        if (amount == 1)
        {
            Destroy();
            return true;
        }
        return false;
    }

    //�ϳ� �ı�
    public bool Destroy()
    {
        if (amount > 1)
        {
            amount--;
            return true;
        }
        if (amount == 1)
        {
            //DESTROY TODO
            return true;
        }
        return false;
    }
}
