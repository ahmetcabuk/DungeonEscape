using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItem : InteractableBase
{
    public Sprite requireItemSprite;
    public bool shouldDestroy = false;
    public bool itemUsed = false;
    public string useItemAudioName;

    public override void Interact()
    {
        if (SortItems.spriteList.Contains(requireItemSprite))
        {
            for (int i = 0; i < SortItems.spriteList.Count; i++)
            {
                if (SortItems.spriteList[i] == requireItemSprite)
                {
                    AudioManager.Instance?.PlaySFXAudio2D(useItemAudioName);
                    SortItems.itemSlotList[SortItems.spriteList.Count - 1].color = new Color(SortItems.itemSlotList[i].color.r, SortItems.itemSlotList[i].color.g, SortItems.itemSlotList[i].color.b, 0f);
                    SortItems.spriteList.RemoveAt(i);
                    itemUsed = true;

                    if (shouldDestroy)
                    {
                        Destroy(gameObject);
                    }

                }
            }
        }
    }

}
