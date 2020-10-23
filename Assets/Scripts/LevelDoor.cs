using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoor : InteractableBase
{
    public GameObject levelEndImage;
    public Sprite requireItemSprite;
    private Animator levelEndAnimator;
    public bool itemUsed = false;

    private void Start()
    {
        levelEndAnimator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        if (SortItems.spriteList.Contains(requireItemSprite))
        {
            for (int i = 0; i < SortItems.spriteList.Count; i++)
            {
                if (SortItems.spriteList[i] == requireItemSprite)
                {
                    SortItems.itemSlotList[SortItems.spriteList.Count - 1].color = new Color(SortItems.itemSlotList[i].color.r, SortItems.itemSlotList[i].color.g, SortItems.itemSlotList[i].color.b, 0f);
                    SortItems.spriteList.RemoveAt(i);
                    itemUsed = true;

                    levelEndImage.SetActive(true);
                    Debug.Log("Level Complete");
                }
            }
        }
    }

}
