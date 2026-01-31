using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class EndingManager : MonoBehaviour
{
    public static EndingManager Instance;

    [Header("Videos")]
    [SerializeField] private VideoClip goodEnding;
    [SerializeField] private VideoClip badEnding;
    [SerializeField] private VideoClip maskEnding;

    [Header("Conditions")]
    [SerializeField] private int totalMemoriesRequired = 6;


    public bool IntroPlayed { get; private set; }
public bool IsIntroPlaying { get; private set; }
    private bool endingForced;
    public bool IsEndingPlaying { get; private set; }
    public EndingType LastEnding { get; private set; }


    public void OnEndingStarted()
{
    IsEndingPlaying = true;

    if (MaskAttackManager.Instance != null)
        MaskAttackManager.Instance.DisablePermanently();

    if (MemoryManager.Instance != null)
        MemoryManager.Instance.OnGameEnded();
}
public void StartIntro()
{
    IsIntroPlaying = true;
}

public void MarkIntroPlayed()
{
    IntroPlayed = true;
    IsIntroPlaying = false;
}

    void Awake()
    {
        Debug.Log("EndingManager Awake");
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ForceEnding()
    {
        endingForced = true;
        IsEndingPlaying = true;
        LastEnding = EndingType.Mask;
        PlayEnding();

    }

    public void PlayEnding()
    {
        OnEndingStarted();
        if (MaskAttackManager.Instance != null)
    {
        MaskAttackManager.Instance.DisablePermanently();
    }

        if (endingForced)
        {
            LastEnding = EndingType.Mask;
            Play(maskEnding);
            return;
        }
        // ’Œ–Œÿ¿ﬂ ÍÓÌˆÓ‚Í‡
        if (MemoryManager.Instance.CollectedCount >= totalMemoriesRequired)
        {
            LastEnding = EndingType.Good; 
            Play(goodEnding);
            return;
        }
        else // œÀŒ’¿ﬂ
        {
            LastEnding = EndingType.Bad;
            Play(badEnding);
            return;
        }
    }

    private void Play(VideoClip clip)
    {
        VideoSequence.Instance.Play(
            clip: clip,
            allowSkip: true,
            quitAtEnd: true
        );
    }

    public static void DisableAllRaycastersExcept(Canvas allowed)
    {
        GraphicRaycaster[] raycasters =
            Object.FindObjectsOfType<GraphicRaycaster>(true);

        foreach (var r in raycasters)
        {
            if (allowed == null || r.GetComponentInParent<Canvas>() != allowed)
                r.enabled = false;
        }
    }
}
