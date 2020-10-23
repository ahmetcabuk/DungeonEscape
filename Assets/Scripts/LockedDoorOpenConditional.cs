using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorOpenConditional : InteractableBase
{
    public UseItem attachObject;
    public static bool conditionOpen = false;
    public float doorAngle;
    public float openingTime = 3;
    public float doorFirstAngle;
    private bool isOpen = false;

    void Start()
    {
        doorFirstAngle = transform.eulerAngles.y;
    }

    public override void Interact()
    {
        conditionOpen = attachObject.itemUsed;

        if (conditionOpen)
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
    }
}
