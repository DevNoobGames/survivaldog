using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public enum ItemType {
        Wood,
        Stone,
        Random,
    }
    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType) { 
            default:
            case ItemType.Wood: return ItemAssets.Instance.woodSprite;
            case ItemType.Stone: return ItemAssets.Instance.stoneSprite;
        }
    }
}
