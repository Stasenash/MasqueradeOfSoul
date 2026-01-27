using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnBlocker : MonoBehaviour
{
    void Start()
    {
        if (GameState.Instance.outsideEntered)
        {
            gameObject.SetActive(false);
        }
    }
}
