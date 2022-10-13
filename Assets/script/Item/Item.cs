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
    //타입
    public ItemType type = ItemType.NULL;

    //이름
    public string name;

    //이미지
    public Sprite img;

    //효과
    public List<ItemEft> efts;

    //갯수
    public int amount;

    //상위아이템
    public string[] hightTierItems;

    //이 아이템을 만드는데 필요한재료
    public string[] needItems;

    //소모할 재료 연결
    public List<int> connectingNeedItemSlot = new();

    //사용
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
    //던지기
    public bool Throw()
    {
        Debug.Log(name + " : Throw()");
        return false;
    }
    //버리기
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

    //하나 파괴
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
