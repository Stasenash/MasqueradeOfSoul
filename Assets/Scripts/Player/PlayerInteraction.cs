using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Interactable current;

    void Update()
    {
        if (current != null && Input.GetKeyDown(KeyCode.E))
        {
            current.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Interactable interactable))
        {
            current = interactable;
            current.OnFocus();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Interactable interactable))
        {
            if (current == interactable)
            {
                current.OnUnfocus();
                current = null;
            }
        }
    }
}
