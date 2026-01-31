using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private GameObject maskAttackPrefab;
    [SerializeField] private GameObject endingManagerPrefab;

    void Awake()
    {
        Instantiate(endingManagerPrefab);
        Instantiate(maskAttackPrefab);
    }
}
