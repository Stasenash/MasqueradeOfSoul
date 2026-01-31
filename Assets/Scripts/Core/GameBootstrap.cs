using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private GameObject maskAttackPrefab;
    [SerializeField] private GameObject endingManagerPrefab;
    [SerializeField] private GameObject videoSequencePrefab;
    [SerializeField] private GameObject endingResultUIPrefab;
    [SerializeField] private GameObject globalEventSystemPrefab;
    [SerializeField] private GameObject audioManagerPrefab;
    [SerializeField] private GameObject resetManagerPrefab;

    void Awake()
    {
        Instantiate(audioManagerPrefab);
        if (FindObjectOfType<GlobalEventSystem>() == null)
            Instantiate(globalEventSystemPrefab);
        Instantiate(videoSequencePrefab);
        Instantiate(endingManagerPrefab);
        Instantiate(maskAttackPrefab);
        Instantiate(endingResultUIPrefab);
        if (FindObjectOfType<ResetManager>() == null)
            Instantiate(resetManagerPrefab);
    }
}
