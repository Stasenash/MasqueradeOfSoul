using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class EndingResultUI : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;

     void Awake()
    {
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false);
    }

    public void Show(EndingType ending)
    {
        Time.timeScale = 1f;

        Canvas myCanvas = GetComponentInChildren<Canvas>();
    EndingManager.DisableAllRaycastersExcept(myCanvas);

    AudioListener.pause = false;


    var raycaster = GetComponentInChildren<UnityEngine.UI.GraphicRaycaster>();
    if (raycaster != null)
        raycaster.enabled = true;

        gameObject.SetActive(true);
        switch (ending)
        {
            case EndingType.Good:
                titleText.text = "Вы прошли игру";
                descriptionText.text = "Ваша концовка: Хорошая";
                break;

            case EndingType.Mask:
                titleText.text = "Вы прошли игру";
                descriptionText.text = "Ваша концовка: Марионетка";
                break;
            case EndingType.Bad:
                titleText.text = "Вы прошли игру";
                descriptionText.text = "Ваша концовка: Плохая";
                break;
        }
    }

    public void GoToMenu()
    {
        gameObject.SetActive(false);
        ResetManager.Instance.FullResetAndGoToMenu();
    }

    private IEnumerator LoadMenuSafe()
{
    yield return null; // дать Unity закончить текущий кадр

    UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
}

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
