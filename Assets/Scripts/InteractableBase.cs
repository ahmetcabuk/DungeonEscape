using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();
}

[RequireComponent(typeof(Collider))]
public abstract class InteractableBase : MonoBehaviour, IInteractable
{
    public bool interactable = true;
    public bool accessable = false;

    protected virtual void OnEnable()
    {
        InteractionButton.Instance?.OnInteraction.AddListener(OnInteract);
    }

    protected virtual void OnDisable()
    {
        InteractionButton.Instance?.OnInteraction.RemoveListener(OnInteract);
    }

    protected virtual void OnInteract()
    {
        if (!interactable) return;
        if (!accessable) return;
        Interact();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RayCast")
        {
            accessable = true;
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.tag == "RayCast")
        {
            accessable = false;
        }
    }

    public abstract void Interact();
}
