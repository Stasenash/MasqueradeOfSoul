using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : Interactable
{
    public override void Interact()
    {
        Debug.Log("Interact!");
    }
}
