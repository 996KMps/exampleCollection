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
    public List<int> connectingNeedItemSlot = new List<int>();

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
        Debug.Log(name + " : Dump()");
        return false;
    }

    //�ϳ� �ı�
    public bool Destroy()
    {
        Debug.Log(name + " : Destroy()");
        type = ItemType.NULL;
        img = null;
        return true;
    }
    //���� �ı�
    public bool DestroyAll()
    {
        Debug.Log(name + " : DestroyAll()");
        return true;
    }
}
