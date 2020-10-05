using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickUp
{
    void PickUp();
}

public abstract class PickUpBase : InteractableBase, IPickUp
{
    public override void Interact()
    {
        PickUp();
    }

    public abstract void PickUp();
}
