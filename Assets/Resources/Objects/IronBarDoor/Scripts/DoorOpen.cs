using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorOpen : MonoBehaviour
{
    public bool interactable;
    private bool accessable = false;
    public float doorAngle;
    public float openingTime = 3;
    private bool isOpen = false;
    public float doorFirstAngle;


    void Start()
    {
        doorFirstAngle = transform.eulerAngles.y;
    }

    private void OnEnable()
    {
        InteractionButton.Instance?.OnInteraction.AddListener(OnInteraction);
    }

    private void OnDisable()
    {
        InteractionButton.Instance?.OnInteraction.RemoveListener(OnInteraction);
    }

    private void OnInteraction()
    {
        if (interactable && accessable && !isOpen)
        {
            OpenGate();
        }
        else if ((interactable && accessable && isOpen))
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            accessable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            accessable = false;
        }
    }
}
