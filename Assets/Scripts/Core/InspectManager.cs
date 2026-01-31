using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectManager : MonoBehaviour
{
   public static InspectManager Instance;
   public event Action OnInspectClosed;

    [SerializeField] private Canvas canvas;
    [SerializeField] private Image inspectImage;
    [SerializeField] private Image background;

    private bool isActive;
    public bool IsInspecting;

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

    void Update()
    {
        if (!isActive) return;

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            Close();
        }
    }

    public void Open(Sprite sprite)
    {
        inspectImage.sprite = sprite;
        canvas.gameObject.SetActive(true);
        background.gameObject.SetActive(true);
        isActive = true;

        Time.timeScale = 0f;
    }

    public void Close()
    {
        canvas.gameObject.SetActive(false);
        background.gameObject.SetActive(false);
        inspectImage.sprite = null;
        isActive = false;

        Time.timeScale = 1f;
        OnInspectClosed?.Invoke();
    }
}
