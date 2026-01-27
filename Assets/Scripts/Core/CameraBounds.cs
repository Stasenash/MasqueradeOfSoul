using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    [SerializeField] private SpriteRenderer wallRenderer;
    private Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }

    public float ClampX(float x)
    {
        if (wallRenderer == null) return x;

        Bounds b = wallRenderer.bounds;

        float halfWidth = cam.orthographicSize * cam.aspect;

        float minX = b.min.x + halfWidth;
        float maxX = b.max.x - halfWidth;

        return Mathf.Clamp(x, minX, maxX);
    }
}
