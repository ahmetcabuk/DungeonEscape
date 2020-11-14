using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorOpen : InteractableBase
{
    public float doorAngle;
    public float openingTime = 3;
    public float doorFirstAngle;
    public string doorOpenAudioName;
    private bool isOpen = false;


    void Start()
    {
        doorFirstAngle = transform.eulerAngles.y;
    }

    public override void Interact()
    {
        if (!isOpen)
        {
            OpenGate();
        }
        else if (isOpen)
        {
            CloseGate();
        }
    }
    
    private void CloseGate()
    {
        transform.DORotate(new Vector3(0, doorFirstAngle, 0), openingTime);
        isOpen = false;
    }

    private void OpenGate()
    {
        transform.DORotate(new Vector3(0, doorAngle, 0), openingTime);
        isOpen = true;
        AudioManager.Instance?.PlaySFXAudio3D(doorOpenAudioName, gameObject.transform.position);
    }
}
