using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : Interactable
{
    private SpriteRenderer sr;
    private Color baseColor;
    [SerializeField] GameObject eIcon;

    void Awake()
    {
        eIcon.SetActive(false);
        sr = GetComponent<SpriteRenderer>();
        baseColor = sr.color;
    }

    public override void OnFocus()
    {
        sr.color = Color.gray;
        if (eIcon) eIcon.SetActive(true);
    }

    public override void OnUnfocus()
    {
        sr.color = baseColor;
        if (eIcon) eIcon.SetActive(false);
    }

    public override void Interact()
    {
        Debug.Log("Interact!");
    }
}
