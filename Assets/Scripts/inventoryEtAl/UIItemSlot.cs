using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIItemSlot : MonoBehaviour, IPointerDownHandler
{
    public int SlotNumber;
    public GameObject dragObject;
    public UIItemSlotDragger slotDragger;
    public craftingMenu crafter;

    public string itemName;
    public int itemInIdList;
    public bool isTool;
    public bool isFarm;
    public int amount;
    public Image spriteImageObj;
    public Sprite correspondingSprite;

    public TextMeshProUGUI slotAmount;

    public float range;
    public float damagePower;
    public float choppingPower;
    public float axePower;
    public float diggingPower;
    public float reloadTime;


    public void OnPointerDown(PointerEventData eventData) // 3
    {
        if (amount > 0)
        {
            dragObject.SetActive(true);
            slotDragger.oldUI = gameObject;
            slotDragger.SlotNumber = SlotNumber;
            slotDragger.itemInIdList = itemInIdList;
            slotDragger.itemName = itemName;
            slotDragger.isTool = isTool;
            slotDragger.amount = amount;
            slotDragger.amountText.text = amount.ToString();
            slotDragger.spriteImageObj.sprite = correspondingSprite;
            slotDragger.correspondingSprite = correspondingSprite;
            slotDragger.spriteImageObj.enabled = true;
            slotDragger.range = range;
            slotDragger.damagePower = damagePower;
            slotDragger.choppingPower = choppingPower;
            slotDragger.axePower = axePower;
            slotDragger.diggingPower = diggingPower;
            slotDragger.reloadTime = reloadTime;
            slotDragger.isFarm = isFarm;

            slotAmount.enabled = false;
            spriteImageObj.enabled = false;

  
            /*if (name == "Result") //IS THE CRAFT RESULT SLOT
            {
                foreach (UIItemSlot slot in crafter.itemslots)
                {
                    for (int i = 0; i < crafter.itemsToDestroy.Count; i++)
                    {
                        if (slot.itemInIdList == crafter.itemsToDestroy[i].classID)
                        {
                            slot.amount -= crafter.itemsToDestroy[i].classAmount;
                        }
                    }

                    //slotDragger.ClearASlot(slot);
                }
            }*/
        }
        else
        {
            //Debug.Log("has nada");
        }
    }

    public void removeItemsFromCraft()
    {
        foreach (UIItemSlot slot in crafter.itemslots)
        {
            for (int i = 0; i < crafter.itemsToDestroy.Count; i++)
            {
                if (slot.itemInIdList == crafter.itemsToDestroy[i].classID)
                {
                    slot.amount -= crafter.itemsToDestroy[i].classAmount;
                }
            }
        }
    }
}
