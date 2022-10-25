//Attach this script to the GameObject you would like to have mouse clicks detected on
//This script outputs a message to the Console when a click is currently detected or when it is released on the GameObject with this script attached

using UnityEngine;
using UnityEngine.EventSystems;

public class Example : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    //Detect current clicks on the GameObject (the one with the script attached)
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log(name + "Game Object Right Click in Progress");
        }
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log(name + "Game Object left Click in Progress");
        }
        if (pointerEventData.button == PointerEventData.InputButton.Middle)
        {
            Debug.Log(name + "Game Object Mid Click in Progress");
        }
        //Output the name of the GameObject that is being clicked
        Debug.Log(name + "Game Object Click in Progress");
    }

    //Detect if clicks are no longer registering
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        Debug.Log(name + "No longer being clicked");
    }
}