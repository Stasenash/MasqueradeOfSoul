using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MemoryItem : InspectableItem
{
    [SerializeField] private string memoryId;
    [SerializeField] private VideoClip memoryVideo;

    [Header("Ending")]
    [SerializeField] private bool isEndingTrigger;

    public override void Interact()
    {
        InspectManager.Instance.OnInspectClosed += OnInspectClosed;
        base.Interact();
        Debug.Log("hi");
        //if (MemoryManager.Instance.IsMemoryCollected(memoryId))
            //return;
    }

    private void OnInspectClosed()
    {
        Debug.Log("Cutscene");
        InspectManager.Instance.OnInspectClosed -= OnInspectClosed;
        if (MemoryManager.Instance.IsMemoryCollected(memoryId))
            return;
        MemoryManager.Instance.CollectMemory(memoryId);
        StartCoroutine(PlayVideoNextFrame());
    }

    private IEnumerator PlayVideoNextFrame()
    {
        yield return null; // ← КРИТИЧЕСКИ ВАЖНО
        if (isEndingTrigger)
        {
            EndingManager.Instance.PlayEnding();
        }
        if (memoryVideo != null)
        {
            VideoSequence.Instance.Play(
                clip: memoryVideo,
                allowSkip: true,
                quitAtEnd: false
            );
        }
    }
}
