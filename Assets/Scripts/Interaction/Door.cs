using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private string targetScene;
    [SerializeField] private string targetSpawnId;
    [SerializeField] private bool requiresConfirmation;

    public override void Interact()
    {
        SpawnManager.Instance.nextSpawnId = targetSpawnId;
        if (requiresConfirmation)
        {
            ExitConfirmController.Instance.Show(targetScene);
            return;
        }
        Debug.Log("Set spawn: " + targetSpawnId);
        SceneLoader.Instance.LoadScene(targetScene);
    }
}
