using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MaskAttackManager : MonoBehaviour
{
    public static MaskAttackManager Instance;

    [Header("UI")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image maskImage;

    [Header("Mask Difficulty (Fixed)")]
[SerializeField] private int fixedClicks = 15;
[SerializeField] private float fixedDuration = 2.5f;
[SerializeField] private float movementStrength = 0.4f;
[SerializeField] private float baseMaskScale = 0.85f;
private Vector3 baseScale;


    [Header("Timing")]
    [SerializeField] private float startDelay = 10f;
    [SerializeField] private float baseDuration = 3f;
    [SerializeField] private float minDuration = 0.8f;

    [SerializeField] private string[] disabledScenes =
{
    "MainMenu",
    "Loader"
};

[SerializeField] private float minInterval = 4f;

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
                float interval =
    Mathf.Lerp(
        startDelay,
        minInterval,
        1f - Mathf.Exp(-attackCount * 0.4f)
    );
            yield return new WaitForSeconds(interval);
        }
    }

    // =========================
    // ATTACK
    // =========================

    private IEnumerator TriggerAttack()
{
    attackCount++;

    requiredClicks = fixedClicks;
    currentClicks = 0;
    active = true;

    canvas.gameObject.SetActive(true);
    baseScale = Vector3.one * baseMaskScale;
maskImage.transform.localScale = baseScale;

    maskRect.anchoredPosition = basePosition;

    float timer = fixedDuration;

    while (timer > 0f && currentClicks < requiredClicks)
    {
        timer -= Time.unscaledDeltaTime;

        // лёгкое движение, всегда одинаковое
        Vector2 offset = new Vector2(
            Mathf.Sin(Time.unscaledTime * 10f),
            Mathf.Cos(Time.unscaledTime * 6f)
        ) * 20f * movementStrength;

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

    bool mouseClick = Input.GetMouseButtonDown(0);
    bool spacePress = Input.GetKeyDown(KeyCode.Space);

    if (mouseClick || spacePress)
    {
        // пробел менее эффективен
        int power = mouseClick ? 1 : 1;

        currentClicks += power;

        maskImage.transform.localScale =
    baseScale * (1f + Random.Range(-0.05f, 0.05f));

    }
    }

    // =========================
    // FAIL
    // =========================

    private void Fail()
    {
        Debug.Log("MASK TOOK YOU");
        EndingManager.Instance.ForceEnding();
    }

    // =========================
    // BLOCKERS
    // =========================

    private bool CanAttack()
    {
        string sceneName = SceneManager.GetActiveScene().name;

    foreach (var s in disabledScenes)
    {
        if (sceneName == s)
            return false;
    }
        if (EndingManager.Instance != null &&
            EndingManager.Instance.IsEndingPlaying)
            return false;
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

    public void DisablePermanently()
{
    StopAllCoroutines();
    active = false;
    enabled = false;

    if (canvas != null)
        canvas.gameObject.SetActive(false);
}

}
