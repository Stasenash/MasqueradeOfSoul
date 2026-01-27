using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectableItem : Interactable
{
    // Start is called before the first frame update
    [SerializeField] private Sprite inspectSprite;
    private Color baseColor;

    public override void Interact()
    {
        InspectManager.Instance.Open(inspectSprite);
    }
}
