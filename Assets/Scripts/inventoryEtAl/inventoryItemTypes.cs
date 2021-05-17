using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryItemTypes : MonoBehaviour
{
    public itemType[] itemtypes;
}


[System.Serializable]
public class itemType
{
    public string itemName;
    public Sprite inventorySprite;
    public bool isTool;
    public bool isFarm;

    [Header ("if tool")]
    public float range;
    public float damagePower;
    public float choppingPower;
    public float axePower;
    public float diggingPower;
    public float reloadTime;
}