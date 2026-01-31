using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MaskAttackManager : MonoBehaviour
{
    public static MaskAttackManager Instance;

    [Header("UI")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image maskImage;

    [Header("Timing")]
    [SerializeField] private float startDelay = 20f;
    [SerializeField] private float baseDuration = 3f;
    [SerializeField] private float minDuration = 0.8f;

    [Header("Difficulty")]
    [SerializeField] private int baseClicks = 5;

    private int attackCount;
    private int requiredClicks;
    private int currentClicks;
    private bool active;

    private AnimationCurve durationCurve;
    private AnimationCurve clicksCurve;
    private AnimationCurve movementCurve;

    private RectTransform maskRect;
    private Vector2 basePosition;

void Awake()
{
    if (canvas == null || maskImage == null)
    {
        Debug.LogError("MaskAttackManager: Canvas or MaskImage NOT assigned");
        enabled = false;
        return;
    }

    maskRect = maskImage.rectTransform;

    if (Instance != null)
    {
        Destroy(gameObject);
        return;
    }

    Instance = this;

    if (transform.parent != null)
    {
        Debug.LogError("MaskAttackManager is NOT root!");
        return;
    }

    DontDestroyOnLoad(gameObject);

    InitCurves();
    canvas.gameObject.SetActive(false);
}

    void Start()
    {
        StartCoroutine(AttackLoop());
    }

    // =========================
    // CURVES
    // =========================

    private void InitCurves()
    {
        durationCurve = new AnimationCurve(
            new Keyframe(0f, 1.0f),
            new Keyframe(3f, 0.7f),
            new Keyframe(6f, 0.45f),
            new Keyframe(10f, 0.25f)
        );

        clicksCurve = new AnimationCurve(
            new Keyframe(0f, 1.0f),
            new Keyframe(3f, 1.3f),
            new Keyframe(6f, 1.8f),
            new Keyframe(10f, 2.5f)
        );

        movementCurve = new AnimationCurve(
            new Keyframe(0f, 0.0f),
            new Keyframe(3f, 0.2f),
            new Keyframe(6f, 0.5f),
            new Keyframe(10f, 1.0f)
        );

        Smooth(durationCurve);
        Smooth(clicksCurve);
        Smooth(movementCurve);
    }

    private void Smooth(AnimationCurve curve)
    {
        for (int i = 0; i < curve.length; i++)
            curve.SmoothTangents(i, 0f);
    }

    // =========================
    // LOOP
    // =========================

    private IEnumerator AttackLoop()
    {
        yield return new WaitForSeconds(startDelay);

        while (true)
        {
            if (CanAttack())
                yield return StartCoroutine(TriggerAttack());

            yield return new WaitForSeconds(
                Mathf.Max(5f, startDelay - attackCount * 2f)
            );
        }
    }

    // =========================
    // ATTACK
    // =========================

    private IEnumerator TriggerAttack()
    {
        attackCount++;

        float t = attackCount;

        float duration = Mathf.Max(
            minDuration,
            baseDuration * durationCurve.Evaluate(t)
        );

        requiredClicks = Mathf.CeilToInt(
            baseClicks * clicksCurve.Evaluate(t)
        );

        float movementStrength = movementCurve.Evaluate(t);

        currentClicks = 0;
        active = true;

        canvas.gameObject.SetActive(true);
        maskRect.anchoredPosition = basePosition;

        float timer = duration;

        while (timer > 0f && currentClicks < requiredClicks)
        {
            timer -= Time.unscaledDeltaTime;

            // ÄÂÈÆÅÍÈÅ ÌÀÑÊÈ
            Vector2 offset = new Vector2(
                Mathf.Sin(Time.unscaledTime * 11f),
                Mathf.Cos(Time.unscaledTime * 7f)
            ) * 25f * movementStrength;

            maskRect.anchoredPosition = basePosition + offset;

            yield return null;
        }

        canvas.gameObject.SetActive(false);
        active = false;

        if (currentClicks < requiredClicks)
            Fail();
    }

    // =========================
    // INPUT
    // =========================

    void Update()
    {
        if (!active) return;

        if (Input.GetMouseButtonDown(0))
        {
            currentClicks++;

            // ìèêðî-ôèäáåê
            maskImage.transform.localScale =
                Vector3.one * (1f + Random.Range(-0.07f, 0.07f));
        }
    }

    // =========================
    // FAIL
    // =========================

    private void Fail()
    {
        Debug.Log("MASK TOOK YOU");
        EndingManager.Instance.ForceEnding();


    EndingManager.Instance.PlayEnding();
    }

    // =========================
    // BLOCKERS
    // =========================

    private bool CanAttack()
    {
        if (InspectManager.Instance != null &&
            InspectManager.Instance.IsInspecting)
            return false;

        if (VideoSequence.Instance != null &&
            VideoSequence.Instance.IsPlaying)
            return false;

        if (ExitConfirmController.Instance != null &&
            ExitConfirmController.Instance.IsOpen)
            return false;

        return true;
    }
}
