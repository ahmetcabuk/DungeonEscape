using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTorch : PickUpBase
{
    public GameObject handTorch;

    public override void PickUp()
    {
        if (!handTorch.activeInHierarchy)
        {
            Debug.Log("Torch aldin");
            handTorch.SetActive(true);
            Destroy(transform.parent.gameObject);
        }
        else if (handTorch.activeInHierarchy)
        {
            Debug.Log("Torch'un var amjik");
        }
    }
}
