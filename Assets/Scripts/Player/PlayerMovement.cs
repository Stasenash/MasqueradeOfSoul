using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    private Animator animator;
    private Rigidbody2D rb;
    private float inputX;
    private Vector3 baseScale;

    private float stepTimer;
    [SerializeField] private float stepInterval = 0.4f;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        baseScale = transform.localScale;
    }

    void Update()
    {
        if (Time.timeScale == 0f)
        {
            animator.SetFloat("Speed", 0f);
            return;
        }

        inputX = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(inputX) > 0.1f)
    {
        stepTimer -= Time.deltaTime;
        if (stepTimer <= 0f)
        {
            AudioManager.Instance.PlayFootstep();
            stepTimer = 0.4f;
        }
    }

        // Разворот
        if (Mathf.Abs(inputX) > 0.01f)
        {
            transform.localScale = new Vector3(
                baseScale.x * Mathf.Sign(inputX),
                baseScale.y,
                baseScale.z
            );
        }

        // Анимация: используем РЕАЛЬНОЕ движение
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(inputX * speed, 0f);
    }
}
