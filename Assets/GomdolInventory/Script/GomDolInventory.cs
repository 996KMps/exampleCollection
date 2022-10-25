using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GomDolInventory : MonoBehaviour
{
    #region Singleton  
    public static GomDolInventory instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnSlotCountChange(int val);
    public OnSlotCountChange onSlotCountChange;

    private int slotCnt;

    public int SlotCnt
    { 
        get => slotCnt;
        set
        {
            slotCnt = value;
            onSlotCountChange.Invoke(slotCnt);
        }
    }

    private void Start()
    {
        SlotCnt = 4;
    }
}
