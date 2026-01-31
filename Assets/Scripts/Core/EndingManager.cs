using UnityEngine;
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

    private bool isFail;

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

    public void ForceEnding()
    {
        isFail = true;
    }

    public void PlayEnding()
    {
        if (isFail)
        {
            Play(maskEnding);
        }
        // ’Œ–Œÿ¿ﬂ ÍÓÌˆÓ‚Í‡
        if (MemoryManager.Instance.CollectedCount >= totalMemoriesRequired)
        {
            Play(goodEnding);
        }
        else // œÀŒ’¿ﬂ
        {
            Play(badEnding);
        }
    }

    private void Play(VideoClip clip)
    {
        VideoSequence.Instance.Play(
            clip: clip,
            allowSkip: false,
            quitAtEnd: true
        );
    }
}
