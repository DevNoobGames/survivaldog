using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolbarScript : MonoBehaviour
{
    public PlayerScript playerScr;
    public inventoryItemTypes invenTypes;
    public craftingMenu crafter;

    public UIItemSlot[] slots;
    public RectTransform highlight;
    public int slotIndex = 0;
    public int ActiveItem;

    public SquareFollow square;
    public Transform squareSize;

    private void Start()
    {
        ActiveItem = 0;

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SlotNumber = i;
        }

        UpdateStack();
    }


    public void UpdateStack()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].amount > 0)
            {
                slots[i].slotAmount.text = slots[i].amount.ToString();
                slots[i].slotAmount.enabled = true;
                slots[i].spriteImageObj.sprite = slots[i].correspondingSprite;
                slots[i].spriteImageObj.enabled = true;
            }
            else
            {
                slots[i].itemInIdList = 99999;
                slots[i].spriteImageObj.enabled = false;
                slots[i].slotAmount.enabled = false;
            }
        }
        resetWeaponStats();
        crafter.CheckNumbers();
    }

    public void RemoveItemFromToolbar (int itemNumberInInventoryList, int amount)
    {
        for (int i = 0; i < slots.Length; i++) //run all slots to check if we already have it
        {
            if (slots[i].itemInIdList == itemNumberInInventoryList)
            {
                    slots[i].amount -= amount;
                    UpdateStack();
                    return; //stop the void
            }
        }
    }

    public void AddItemToToolbar(int itemNumberInInventoryList, int amount)
    {
        if (amount == 999)
        {
            int ranval = Random.Range(1, 5);
            amount = ranval;
        }

        for (int i = 0; i < slots.Length; i++) //run all slots to check if we already have it
        {
            if (slots[i].itemInIdList == itemNumberInInventoryList)
            {
                if (!invenTypes.itemtypes[itemNumberInInventoryList].isTool) //tools shouldn't be added twice
                {
                    slots[i].amount += amount;
                    UpdateStack();
                    return; //stop the void
                }
                else return; //stop the void if already have tool
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].amount == 0)
            {
                slots[i].itemName = invenTypes.itemtypes[itemNumberInInventoryList].itemName;
                slots[i].itemInIdList = itemNumberInInventoryList;
                slots[i].correspondingSprite = invenTypes.itemtypes[itemNumberInInventoryList].inventorySprite;
                slots[i].isTool = invenTypes.itemtypes[itemNumberInInventoryList].isTool;
                slots[i].amount += amount;
                slots[i].range = invenTypes.itemtypes[itemNumberInInventoryList].range;
                slots[i].damagePower = invenTypes.itemtypes[itemNumberInInventoryList].damagePower;
                slots[i].choppingPower = invenTypes.itemtypes[itemNumberInInventoryList].choppingPower;
                slots[i].axePower = invenTypes.itemtypes[itemNumberInInventoryList].axePower;
                slots[i].diggingPower = invenTypes.itemtypes[itemNumberInInventoryList].diggingPower;
                slots[i].reloadTime = invenTypes.itemtypes[itemNumberInInventoryList].reloadTime;
                slots[i].isFarm = invenTypes.itemtypes[itemNumberInInventoryList].isFarm;
                UpdateStack();
                return;
            }
        }

        return;
    }

    public void resetWeaponStats()
    {
        playerScr.range = slots[ActiveItem].range;
        playerScr.damagePower = slots[ActiveItem].damagePower;
        playerScr.choppingPower = slots[ActiveItem].choppingPower;
        playerScr.axePower = slots[ActiveItem].axePower;
        playerScr.diggingPower = slots[ActiveItem].diggingPower;
        playerScr.reloadTime = slots[ActiveItem].reloadTime;
        playerScr.isFarm = slots[ActiveItem].isFarm;
        if (playerScr.isFarm)
        {
            square.yOffset = 1.5f;
            square.xOffset = 0f;
            squareSize.localScale = new Vector2(2, 3);
        }
        else
        {
            square.yOffset = 0.5f;
            square.xOffset = 0.5f;
            squareSize.localScale = new Vector2(1, 1);
        }
    }

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {

            if (scroll > 0)
                slotIndex--;
            else
                slotIndex++;

            if (slotIndex > 8)
                slotIndex = 0;
            if (slotIndex < 0)
                slotIndex = 8;

            highlight.position = slots[slotIndex].spriteImageObj.transform.position;
            ActiveItem = slotIndex;

            resetWeaponStats();
        }
    }
}
