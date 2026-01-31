using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : Interactable
{
    public override void Interact()
    {
        AudioManager.Instance.PlayInspectItem();
        Debug.Log("Interact!");
    }
}
