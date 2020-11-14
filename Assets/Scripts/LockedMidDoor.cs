using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedMidDoor : InteractableBase
{
    public override void Interact()
    {
        AudioManager.Instance?.PlaySFXAudio2D("LockedDoor");
    }
}
