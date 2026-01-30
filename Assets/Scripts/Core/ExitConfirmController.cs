using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitConfirmController : MonoBehaviour
{
    public static ExitConfirmController Instance;

    [SerializeField] private Canvas canvas;
    private string targetScene;

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

    public void Show(string sceneName)
    {
        targetScene = sceneName;
        Time.timeScale = 0f;
        canvas.gameObject.SetActive(true);
    }

    public void Confirm()
    {
        Time.timeScale = 1f;
        canvas.gameObject.SetActive(false);
        SceneManager.LoadScene(targetScene);
    }

    public void Cancel()
    {
        Time.timeScale = 1f;
        canvas.gameObject.SetActive(false);
    }
}
