using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Collections;

public class VideoSequence : MonoBehaviour
{
    public static VideoSequence Instance;

    [Header("UI")]
    [SerializeField] private Canvas videoCanvas;
    [SerializeField] private RawImage videoImage;
    [SerializeField] private VideoPlayer videoPlayer;

    private bool isPlaying;
    private bool canSkip;
    private bool quitAfter;

    public bool IsPlaying;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        videoCanvas.gameObject.SetActive(false);
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

        StartCoroutine(PlayRoutine(clip));
    }

    private IEnumerator PlayRoutine(VideoClip clip)
    {
        isPlaying = true;
        Time.timeScale = 0f;

        videoPlayer.Stop();
        if (videoPlayer.targetTexture != null)
        {
            var rt = videoPlayer.targetTexture;
            RenderTexture.active = rt;
            GL.Clear(true, true, Color.black);
            RenderTexture.active = null;
        }
        videoCanvas.gameObject.SetActive(true);

        videoPlayer.clip = clip;
        videoPlayer.Prepare();

        while (!videoPlayer.isPrepared)
            yield return null;

        videoPlayer.Play();

        while (videoPlayer.isPlaying)
            yield return null;

        StopVideo();

    }

    private void StopVideo()
    {
        Time.timeScale = 1f;
        videoPlayer.Stop();
        videoCanvas.gameObject.SetActive(false);

        Time.timeScale = 1f;

        if (quitAfter)
        {
            EndingResultUI result = FindObjectOfType<EndingResultUI>(true);
            result.Show(EndingManager.Instance.LastEnding);
        }

        isPlaying = false;

    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}