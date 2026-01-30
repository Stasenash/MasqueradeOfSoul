using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private RuntimeAnimatorController baseController;
    [SerializeField] private AnimatorOverrideController[] maskOverrides;

    private RuntimeAnimatorController current;

    private bool initialized;

    void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        animator.enabled = true;
    }

    public int MaskCount => maskOverrides.Length;

    public void SetMaskStage(int stage)
    {
        RuntimeAnimatorController target;

        if (stage < 0)
            target = baseController;
        else
            target = maskOverrides[Mathf.Clamp(stage, 0, maskOverrides.Length - 1)];

        if (animator.runtimeAnimatorController == target)
            return;

        animator.runtimeAnimatorController = target;
        animator.Update(0f);
    }

    public void ForceApplyStage(int stage)
    {
        initialized = false;
        SetMaskStage(stage);
    }
}
