using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3f;

    private Rigidbody2D rb;
    private float inputX;
    private Vector3 baseScale;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        baseScale = transform.localScale;
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;
        inputX = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(inputX * speed, 0f);

        if (inputX != 0)
        {
            transform.localScale = new Vector3(
                baseScale.x * Mathf.Sign(inputX),
                baseScale.y,
                baseScale.z
            );
        }
    }
}
