using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
   public static SceneLoader Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Start()
    {
        Debug.Log("SceneLoader START");
        StartCoroutine(LoadFlow());
    }

    private IEnumerator LoadFlow()
    {
        yield return null; // 1 кадр, чтобы Bootstrap успел
        Debug.Log("Loading MainMenu");
        // после reset всегда идём в главное меню
        SceneManager.LoadScene("MainMenu");
    }
}
