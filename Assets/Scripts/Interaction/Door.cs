using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private string targetScene;

    public override void Interact()
    {
        SceneLoader.Instance.LoadScene(targetScene);
    }
}
