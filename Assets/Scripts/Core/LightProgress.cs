using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightProgress : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    [SerializeField] private float minIntensity = 0.1f;
    [SerializeField] private float maxIntensity = 1.0f;

    void Update()
    {
        float t = MemoryManager.Instance.Progress01;
        globalLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
    }
}
