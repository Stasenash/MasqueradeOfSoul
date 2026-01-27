using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VignetteController : MonoBehaviour
{
    [SerializeField] private Image vignetteImage;
    [SerializeField] private float startAlpha = 0.9f;
    [SerializeField] private float endAlpha = 0.1f;

    void Update()
    {
        float t = MemoryManager.Instance.Progress01;
        float alpha = Mathf.Lerp(startAlpha, endAlpha, t);

        Color c = vignetteImage.color;
        c.a = alpha;
        vignetteImage.color = c;
    }
}
