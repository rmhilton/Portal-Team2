using System;
using UnityEngine;

// Created by Raymend

public class InteractableButton : InteractableObject
{
    public event Action interactEvent;   // connect other objects to this so they do things when the button is interacted with

    // called when the player interacts with the button
    public override void Interact()
    {
        interactEvent?.Invoke();
    }
}
