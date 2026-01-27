using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private string targetScene;
    [SerializeField] private string targetSpawnId;

    public override void Interact()
    {
        SpawnManager.Instance.nextSpawnId = targetSpawnId;
        SceneLoader.Instance.LoadScene(targetScene);
    }
}
