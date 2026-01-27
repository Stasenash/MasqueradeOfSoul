using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryItem : InspectableItem
{
    private bool collected;

    public override void Interact()
    {
        if (collected) return;

        base.Interact(); // открыть осмотр

        collected = true;
        MemoryManager.Instance.CollectMemory();
    }
}
