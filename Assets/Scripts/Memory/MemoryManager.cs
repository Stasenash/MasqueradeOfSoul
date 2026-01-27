using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryManager : MonoBehaviour
{
    public static MemoryManager Instance;

    [Header("Progress")]
    public int totalMemories = 6;
    public int collectedMemories = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void CollectMemory()
    {
        collectedMemories = Mathf.Clamp(collectedMemories + 1, 0, totalMemories);
        Debug.Log("Memorized!");
    }

    public float Progress01 =>
        totalMemories == 0 ? 0f : (float)collectedMemories / totalMemories;
}
