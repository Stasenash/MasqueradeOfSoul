using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected GameObject eIcon;

    protected virtual void Awake()
    {
        if (eIcon) eIcon.SetActive(false);
    }

    public virtual void SetFocused(bool focused)
    {
        if (eIcon)
            eIcon.SetActive(focused);
    }

    public abstract void Interact();
}
