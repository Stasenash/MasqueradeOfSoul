using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryItem : InspectableItem
{
    [SerializeField] private string memoryId;

    public override void Interact()
    {
        base.Interact();
        if (MemoryManager.Instance.IsMemoryCollected(memoryId))
            return;
        
        MemoryManager.Instance.CollectMemory(memoryId);
    }
}
