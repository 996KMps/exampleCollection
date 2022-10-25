using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slotButton : MonoBehaviour
{

    int num;

    private void Awake()
    {
        num = GetComponent<Slot>().slotNum;
    }
}
