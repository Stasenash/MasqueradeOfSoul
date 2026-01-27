using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime = 0.2f;

    private float velocityX;
    private float fixedY;
    private float fixedZ;

    void Awake()
    {
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (target == null) return;

        float newX = Mathf.SmoothDamp(
            transform.position.x,
            target.position.x,
            ref velocityX,
            smoothTime
        );

        CameraBounds bounds = FindObjectOfType<CameraBounds>();
        if (bounds != null)
            newX = bounds.ClampX(newX);

        transform.position = new Vector3(newX, fixedY, fixedZ);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
