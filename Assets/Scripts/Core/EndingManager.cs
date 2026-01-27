using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public static EndingManager Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public EndingType GetEnding()
    {
        if (MemoryManager.Instance.collectedMemories >=
            MemoryManager.Instance.totalMemories)
            return EndingType.Good;

        return EndingType.Bad;
    }
}
