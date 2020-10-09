using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortItems : MonoBehaviour
{
    public static List<Image> itemSlotList = new List<Image>();
    public static List<Sprite> spriteList = new List<Sprite>();
    public Image itemSlot1;
    public Image itemSlot2;
    public Image itemSlot3;

    private void Start()
    {
        itemSlotList.Add(itemSlot1);
        itemSlotList.Add(itemSlot2);
        itemSlotList.Add(itemSlot3);
    }

    private void Update()
    {
        if (spriteList.Count > 0)
        {
            for (int i = 0; i < spriteList.Count; i++)
            {
                itemSlotList[i].sprite = spriteList[i];
                itemSlotList[i].color = new Color(itemSlotList[i].color.r, itemSlotList[i].color.g, itemSlotList[i].color.b, 1f);
            }
        }
    }
}
