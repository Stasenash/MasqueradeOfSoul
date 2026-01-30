using UnityEngine;
using UnityEngine.Video;

public class EndingManager : MonoBehaviour
{
    public static EndingManager Instance;

    [Header("Videos")]
    [SerializeField] private VideoClip goodEnding;
    [SerializeField] private VideoClip badEnding;

    [Header("Conditions")]
    [SerializeField] private int totalMemoriesRequired = 6;

    void Awake()
    {
        Instance = this;
    }

    public void PlayEnding()
    {
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
