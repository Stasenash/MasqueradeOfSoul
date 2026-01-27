using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryItem : InspectableItem
{
    private bool collected;

    public override void Interact()
    {
        base.Interact(); // открыть осмотр
        if (collected) return;

        collected = true;
        MemoryManager.Instance.CollectMemory();
    }
}
