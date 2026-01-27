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
        if (!other.TryGetComponent(out Interactable interactable))
            return;

        SetCurrent(interactable);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Interactable interactable))
            return;

        if (current == interactable)
        {
            ClearCurrent();
        }
    }

    private void SetCurrent(Interactable interactable)
    {
        if (current == interactable)
            return;

        if (current != null)
            current.SetFocused(false);

        current = interactable;
        current.SetFocused(true);
    }

    private void ClearCurrent()
    {
        if (current != null)
            current.SetFocused(false);

        current = null;
    }

    // ÊĞÈÒÈ×ÍÎ: ñòğàõîâêà
    private void OnDisable()
    {
        ClearCurrent();
    }
}
