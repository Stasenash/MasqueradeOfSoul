using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillsItem : InspectableItem
{
    private bool triggered;

    public override void Interact()
    {
        if (triggered) return;

        GameState.Instance.UsePill();

        if (GameState.Instance.pillsUsed >= 3)
        {
            triggered = true;
            FinalSequence.Instance.Play(EndingType.Secret);
        }
    }
}
