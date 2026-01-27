using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideDoor : Interactable
{
    [SerializeField] private string outsideScene = "Outside";

    public override void Interact()
    {
        GameState.Instance.MarkOutsideEntered();
        SceneLoader.Instance.LoadScene(outsideScene);
    }
}
