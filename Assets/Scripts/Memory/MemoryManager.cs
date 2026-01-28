using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryManager : MonoBehaviour
{
    public static MemoryManager Instance;

    [Header("Progress")]
    public int totalMemories = 6;

    private HashSet<string> collectedMemoryIds = new HashSet<string>();

    private PlayerVisualController visuals;
    private int currentStage = -1;

    private bool initialized = false;

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

    void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(
        UnityEngine.SceneManagement.Scene scene,
        UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        StartCoroutine(ApplyMaskNextFrame());
    }

    private IEnumerator ApplyMaskNextFrame()
    {
        yield return null;

        visuals = FindObjectOfType<PlayerVisualController>();
        int stage = CalculateCurrentStage();
        visuals.ForceApplyStage(stage);
    }

    public bool IsMemoryCollected(string id)
    {
        return collectedMemoryIds.Contains(id);
    }

    public void CollectMemory(string id)
    {
        if (!collectedMemoryIds.Add(id))
            return;

        UpdateMaskStage();
    }

    private int CalculateCurrentStage()
    {
        int collected = CollectedCount;

        if (collected == 0)
            return -1;

        return Mathf.Clamp(
            collected - 1,
            0,
            visuals.MaskCount - 1
        );
    }

    private void UpdateMaskStage()
    {
        if (visuals == null) return;

        int stage = CalculateCurrentStage();

        if (stage == currentStage)
            return;

        currentStage = stage;
        visuals.SetMaskStage(stage);
    }


    public int CollectedCount => collectedMemoryIds.Count;

    public float Progress01 =>
        totalMemories == 0
            ? 0f
            : (float)CollectedCount / totalMemories;
}
