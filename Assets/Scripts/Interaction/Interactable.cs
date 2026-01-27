using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public virtual void OnFocus() { }
    public virtual void OnUnfocus() { }
    public abstract void Interact();
}
