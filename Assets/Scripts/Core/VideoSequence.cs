using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class VideoSequence : MonoBehaviour
{
    public static VideoSequence Instance;

    [Header("UI")]
    [SerializeField] private Canvas videoCanvas;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private AudioSource audioSource;

    private bool isPlaying;
    private bool canSkip;
    private bool quitAfter;
    private bool stopped;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (videoCanvas != null)
            videoCanvas.gameObject.SetActive(false);

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isPlaying || !canSkip) return;

        if (Input.GetKeyDown(KeyCode.Escape) ||
            Input.GetKeyDown(KeyCode.Space))
        {
            StopVideo();
        }
    }

    public void Play(VideoClip clip, bool allowSkip, bool quitAtEnd)
    {
        if (isPlaying) return;

        canSkip = allowSkip;
        quitAfter = quitAtEnd;
        stopped = false;

        StartCoroutine(PlayRoutine(clip));
    }

    private IEnumerator PlayRoutine(VideoClip clip)
    {
        isPlaying = true;
        Time.timeScale = 0f;
        AudioListener.pause = false;

        videoPlayer.Stop();

        videoPlayer.clip = clip;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);

        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
            yield return null;

        if (videoCanvas != null)
            videoCanvas.gameObject.SetActive(true);

        videoPlayer.Play();

        while (videoPlayer.isPlaying && !stopped)
            yield return null;

        StopVideo();
    }

    private void StopVideo()
    {
        if (stopped) return;
        stopped = true;

        if (videoPlayer != null)
            videoPlayer.Stop();

        if (videoCanvas != null)
            videoCanvas.gameObject.SetActive(false);

        Time.timeScale = 1f;
        AudioListener.pause = false;

        isPlaying = false;

        if (quitAfter && EndingManager.Instance != null)
        {
            EndingResultUI result =
                FindObjectOfType<EndingResultUI>(true);

            if (result != null)
                result.Show(EndingManager.Instance.LastEnding);
        }
    }

    public bool IsPlaying => isPlaying;
}