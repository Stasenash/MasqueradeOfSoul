using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroCutsceneTrigger : MonoBehaviour
{
    [SerializeField] private string gameplaySceneName = "Bedroom";
    [SerializeField] private VideoClip introClip;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != gameplaySceneName)
            return;

        if (EndingManager.Instance == null)
    return;

if (!EndingManager.Instance.IntroPlayed)
{
    EndingManager.Instance.StartIntro();

    VideoSequence.Instance.Play(
        introClip,
        allowSkip: true,
        quitAtEnd: false
    );
}
           
    }
}
