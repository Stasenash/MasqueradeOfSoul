using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ResetManager : MonoBehaviour
{
    public static ResetManager Instance;

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

    public void FullResetAndGoToMenu()
    {

        // 1. Сброс TimeScale и аудио
        Time.timeScale = 1f;
        AudioListener.pause = false;

        // 2. Убиваем все глобальные менеджеры
        DestroyIfExists<AudioManager>();
        DestroyIfExists<EndingManager>();
        DestroyIfExists<MemoryManager>();
        DestroyIfExists<MaskAttackManager>();
        DestroyIfExists<VideoSequence>();
        DestroyIfExists<GlobalEventSystem>();
        //DestroyIfExists<EndingResultUI>();

        // 3. Загружаем главное меню
        Debug.Log("RESET → LOAD LOADER");
        SceneManager.LoadScene("Loader");
    }

    private void DestroyIfExists<T>() where T : MonoBehaviour
{
        T obj = FindObjectOfType<T>();
        if (obj != null)
            Destroy(obj.gameObject);
}

}
