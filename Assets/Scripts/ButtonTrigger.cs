using System;
using UnityEngine;
using UnityEngine.Events;

/* Class Created By Raymend */

public class ButtonTrigger : MonoBehaviour
{
    private bool pressed = false;

    [SerializeField] private WeightedButton control;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("WeightedButton"))
        {
            pressed = true;
            Debug.Log("Button Pressed!");
            control.buttonPressed?.Invoke(pressed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("WeightedButton"))
        {
            pressed = false;
            Debug.Log("Button Unpressed!");
            control.buttonPressed?.Invoke(pressed);
        }
    }
}
