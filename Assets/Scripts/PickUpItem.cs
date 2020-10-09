using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PickUpItem : PickUpBase
{
    public Sprite sprite;


    public override void PickUp()
    {
        SortItems.spriteList.Add(sprite);
        Destroy(gameObject);
    }
}
