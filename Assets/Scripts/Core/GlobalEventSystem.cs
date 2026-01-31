using UnityEngine;
using UnityEngine.EventSystems;

public class GlobalEventSystem : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
