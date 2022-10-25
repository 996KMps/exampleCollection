using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public static ItemDataBase instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField]
    public List<Item> itemDB = new List<Item>();

    public Item GetItem(int _num)
    {
        Item clone = new();
        Item origin = itemDB[_num];

        clone.type = origin.type;
        clone.name = origin.name;
        clone.img = origin.img;
        clone.efts = origin.efts;
        clone.amount = origin.amount;
        clone.hightTierItems = origin.hightTierItems;
        clone.needItems = origin.needItems;

        return clone;
    }
    
}
