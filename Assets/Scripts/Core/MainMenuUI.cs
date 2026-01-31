using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    void Start()
{
    EndingResultUI result = FindObjectOfType<EndingResultUI>(true);
    if (result != null)
        result.gameObject.SetActive(false);
}

    public void StartGame()
    {
        SceneManager.LoadScene("Bedroom");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}