using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ItemEft/Consumable/Health")]
public class ItemHealingEft : ItemEft
{
    public int healingPoint = 0;
    public override bool ExcuteRole()
    {
        Debug.Log("Heal : " + healingPoint);
        return true;
    }

    
}
