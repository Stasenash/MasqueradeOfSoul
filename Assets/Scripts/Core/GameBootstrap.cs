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

    void Awake()
    {
        Instantiate(globalEventSystemPrefab);
        Instantiate(videoSequencePrefab);
        Instantiate(endingManagerPrefab);
        Instantiate(maskAttackPrefab);
        Instantiate(endingResultUIPrefab);
    }
}
