using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField]
    private GameObject[] slots;

    private int itemCount;

    private void Awake()
    {
        itemCount = 0;

        slots = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            slots[i] = transform.GetChild(i).gameObject;
        }
    }
}
