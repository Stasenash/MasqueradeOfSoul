using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance;
    public int pillsUsed = 0;

    public bool outsideEntered;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void MarkOutsideEntered()
    {
        outsideEntered = true;
    }

    public void UsePill()
    {
        pillsUsed++;
    }

    public bool IsSecretUnlocked => pillsUsed >= 3;
}
