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

        if (EndingManager.IntroPlayed)
            return;

        EndingManager.IntroPlayed = true;

        if (VideoSequence.Instance != null)
        {
            VideoSequence.Instance.Play(
                clip: introClip,
                allowSkip: true,
                quitAtEnd: false
            );
        }
    }
}
