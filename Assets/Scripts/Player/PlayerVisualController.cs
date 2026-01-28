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

        animator.enabled = false;
    }

    public int MaskCount => maskOverrides.Length;

    public void SetMaskStage(int stage)
    {
        RuntimeAnimatorController target;

        if (stage < 0)
            target = baseController;
        else
            target = maskOverrides[Mathf.Clamp(stage, 0, maskOverrides.Length - 1)];

        // ÂÀÆÍÎ: âêëþ÷àåì Animator ÏÅÐÂÛÉ ÐÀÇ ÂÑÅÃÄÀ
        if (!initialized)
        {
            animator.runtimeAnimatorController = target;
            animator.enabled = true;
            animator.Update(0f);

            current = target;
            initialized = true;
            return;
        }

        if (current == target) return;

        current = target;
        animator.runtimeAnimatorController = target;
        animator.Update(0f);
    }

    public void ForceApplyStage(int stage)
    {
        initialized = false;
        SetMaskStage(stage);
    }
}
