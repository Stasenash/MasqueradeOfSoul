using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerBounds : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer wallRenderer;

    private SpriteRenderer playerRenderer;

    void Awake()
    {
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        if (wallRenderer == null) return;

        Bounds room = wallRenderer.bounds;
        Bounds player = playerRenderer.bounds;

        float halfWidth = player.extents.x;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(
            pos.x,
            room.min.x + halfWidth,
            room.max.x - halfWidth
        );

        transform.position = pos;
    }
}
