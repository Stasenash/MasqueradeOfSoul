using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalSequence : MonoBehaviour
{
    public static FinalSequence Instance;

    [SerializeField] private Canvas canvas;
    [SerializeField] private Image image;
    [SerializeField] private float slideDuration = 3f;

    [Header("Slides")]
    [SerializeField] private Sprite[] badEnding;
    [SerializeField] private Sprite[] goodEnding;
    [SerializeField] private Sprite[] secretEnding;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        canvas.gameObject.SetActive(false);
    }

    public void Play(EndingType type)
    {
        Sprite[] slides = type switch
        {
            EndingType.Secret => secretEnding,
            EndingType.Good => goodEnding,
            _ => badEnding
        };

        StartCoroutine(PlaySlides(slides));
    }

    private IEnumerator PlaySlides(Sprite[] slides)
    {
        canvas.gameObject.SetActive(true);
        Time.timeScale = 0f;

        foreach (var slide in slides)
        {
            image.sprite = slide;
            yield return new WaitForSecondsRealtime(slideDuration);
        }

        QuitGame();
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
