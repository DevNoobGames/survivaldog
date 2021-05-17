using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIItemSlotDragger : MonoBehaviour
{
    public ToolbarScript toolbar;
    public TextMeshProUGUI amountText;
    public GameObject oldUI;

    //transfer new 
    public int SlotNumber;
    public string itemName;
    public int itemInIdList;
    public bool isTool;
    public bool isFarm;
    public int amount;
    public Image spriteImageObj;
    public Sprite correspondingSprite;
    public float range;
    public float damagePower;
    public float choppingPower;
    public float axePower;
    public float diggingPower;
    public float reloadTime;

    //store old for transfer
    public int TEMPSlotNumber;
    public string TEMPitemName;
    public int TEMPitemInIdList;
    public bool TEMPisTool;
    public bool TEMPisFarm;
    public int TEMPamount;
    public Sprite TEMPcorrespondingSprite;
    public float TEMPrange;
    public float TEMPdamagePower;
    public float TEMPchoppingPower;
    public float TEMPaxePower;
    public float TEMPdiggingPower;
    public float TEMPreloadTime;

    void Update()
    {
        transform.position = Input.mousePosition;

        if (Input.GetMouseButtonUp(0))
        {
            GameObject closeUI = FindClosestSlotNumber();
            if (Vector3.Distance(closeUI.transform.position, transform.position) < 35 && closeUI != oldUI && closeUI.name != "Result")
            {
                UIItemSlot selectedSlotNumber = closeUI.GetComponent<UIItemSlot>();

                //if new UI has items, send those to mouse,
                if (selectedSlotNumber.amount > 0)
                {
                    //Debug.Log("running here");

                    TEMPSlotNumber = selectedSlotNumber.SlotNumber;
                    TEMPitemName = selectedSlotNumber.itemName;
                    TEMPitemInIdList = selectedSlotNumber.itemInIdList;
                    TEMPisTool = selectedSlotNumber.isTool;
                    TEMPisFarm = selectedSlotNumber.isFarm;
                    TEMPamount = selectedSlotNumber.amount;
                    TEMPcorrespondingSprite = selectedSlotNumber.correspondingSprite;
                    TEMPrange = selectedSlotNumber.range;
                    TEMPdamagePower = selectedSlotNumber.damagePower;
                    TEMPchoppingPower = selectedSlotNumber.choppingPower;
                    TEMPaxePower = selectedSlotNumber.axePower;
                    TEMPdiggingPower = selectedSlotNumber.diggingPower;
                    TEMPreloadTime = selectedSlotNumber.reloadTime;

                    UIItemSlot selectedSlotNumberOLD = oldUI.GetComponent<UIItemSlot>();


                    //Transfer items to new place
                    TransferToSlot(selectedSlotNumber);

                    //Transfer items to new place
                    if (selectedSlotNumberOLD.name != "Result")
                    {
                        Debug.Log("transfer " + TEMPamount);
                        selectedSlotNumberOLD.SlotNumber = TEMPSlotNumber;
                        selectedSlotNumberOLD.itemName = TEMPitemName;
                        selectedSlotNumberOLD.itemInIdList = TEMPitemInIdList;
                        selectedSlotNumberOLD.isTool = TEMPisTool;
                        selectedSlotNumberOLD.isFarm = TEMPisFarm;
                        selectedSlotNumberOLD.amount = TEMPamount;
                        selectedSlotNumberOLD.slotAmount.text = TEMPamount.ToString();
                        selectedSlotNumberOLD.slotAmount.enabled = true;
                        selectedSlotNumberOLD.correspondingSprite = TEMPcorrespondingSprite;
                        selectedSlotNumberOLD.spriteImageObj.sprite = TEMPcorrespondingSprite;
                        selectedSlotNumberOLD.spriteImageObj.enabled = true;
                        selectedSlotNumberOLD.range = TEMPrange;
                        selectedSlotNumberOLD.damagePower = TEMPdamagePower;
                        selectedSlotNumberOLD.choppingPower = TEMPchoppingPower;
                        selectedSlotNumberOLD.axePower = TEMPaxePower;
                        selectedSlotNumberOLD.diggingPower = TEMPdiggingPower;
                        selectedSlotNumberOLD.reloadTime = TEMPreloadTime;
                    }
                    else
                    {
                        ClearASlot(selectedSlotNumberOLD);
                    }

                    ClearThis();
                }


                if (selectedSlotNumber.amount <= 0) //if new position is empyu 
                {
                    //Transfer items to it
                    TransferToSlot(selectedSlotNumber);
                    ClearThis();

                    //Remove from old
                    if (oldUI != null)
                    {
                        UIItemSlot RemoveFrom = oldUI.GetComponent<UIItemSlot>();
                        ClearASlot(RemoveFrom);
                    }

                    checkForDoubles(selectedSlotNumber);
                }

            }
            else                
            {
                //return items to old ui
                UIItemSlot selectedSlotNumber = oldUI.GetComponent<UIItemSlot>();
                TransferToSlot(selectedSlotNumber);

                //reset here
                ClearThis();
            }

            toolbar.UpdateStack();
            toolbar.resetWeaponStats();
        }
    }

    public void checkForDoubles(UIItemSlot selectedSlotNumber)
    {
        for (int i = 0; i < toolbar.slots.Length; i++) //run all slots to check if we already have it
        {
            if (toolbar.slots[i].itemInIdList == selectedSlotNumber.itemInIdList && toolbar.slots[i].name != selectedSlotNumber.name && toolbar.slots[i].name != "Result")
            {
                //Debug.Log(toolbar.slots[i].name + " got " + selectedSlotNumber.amount);
                toolbar.slots[i].amount += selectedSlotNumber.amount;
                ClearASlot(selectedSlotNumber);
                toolbar.UpdateStack();
                return; //stop the void
            }
        }
    }

    public void TransferToSlot(UIItemSlot selectedSlotNumber)
    {
        if (selectedSlotNumber.itemInIdList != itemInIdList)
        {
            selectedSlotNumber.SlotNumber = SlotNumber;
            selectedSlotNumber.itemName = itemName;
            selectedSlotNumber.itemInIdList = itemInIdList;
            selectedSlotNumber.isTool = isTool;
            selectedSlotNumber.isFarm = isFarm;
            selectedSlotNumber.amount = amount;
            selectedSlotNumber.slotAmount.text = amount.ToString();
            selectedSlotNumber.slotAmount.enabled = true;
            selectedSlotNumber.correspondingSprite = correspondingSprite;
            selectedSlotNumber.spriteImageObj.sprite = correspondingSprite;
            selectedSlotNumber.spriteImageObj.enabled = true;
            selectedSlotNumber.range = range;
            selectedSlotNumber.damagePower = damagePower;
            selectedSlotNumber.choppingPower = choppingPower;
            selectedSlotNumber.axePower = axePower;
            selectedSlotNumber.diggingPower = diggingPower;
            selectedSlotNumber.reloadTime = reloadTime;
        }
        else if (selectedSlotNumber != oldUI.GetComponent<UIItemSlot>())
        {
            //Debug.Log(selectedSlotNumber.name + " got " + amount);
            selectedSlotNumber.amount += amount;
        }

        if (selectedSlotNumber.name != "Result" && oldUI.GetComponent<UIItemSlot>().name == "Result")
        {
            selectedSlotNumber.removeItemsFromCraft();
        }
    }

    public void ClearASlot(UIItemSlot removeFromSlot) //RESET ALL TO KEEP IT AS CLEAN AS POSSIBLE
    {
        removeFromSlot.SlotNumber = 0;
        removeFromSlot.itemName = "";
        removeFromSlot.itemInIdList = 0;
        removeFromSlot.isTool = false;
        removeFromSlot.isFarm = false;
        removeFromSlot.amount = 0;
        removeFromSlot.slotAmount.text = "";
        removeFromSlot.slotAmount.enabled = false;
        removeFromSlot.spriteImageObj.sprite = null;
        removeFromSlot.spriteImageObj.enabled = false;
        removeFromSlot.range = 0;
        removeFromSlot.damagePower = 0;
        removeFromSlot.choppingPower = 0;
        removeFromSlot.axePower = 0;
        removeFromSlot.diggingPower = 0;
        removeFromSlot.reloadTime = 0;
    }

    public void ClearThis() //RESET ALL TO KEEP IT AS CLEAN AS POSSIBLE
    {
        SlotNumber = 0;
        itemName = "";
        itemInIdList = 0;
        isTool = false;
        isFarm = false;
        amount = 0;
        correspondingSprite = null;
        range = 0;
        damagePower = 0;
        choppingPower = 0;
        axePower = 0;
        diggingPower = 0;
        reloadTime = 0;

        TEMPSlotNumber = 0;
        TEMPitemName = "";
        TEMPitemInIdList = 0;
        TEMPisTool = false;
        TEMPisFarm = false;
        TEMPamount = 0;
        TEMPcorrespondingSprite = null;
        TEMPrange = 0;
        TEMPdamagePower = 0;
        TEMPchoppingPower = 0;
        TEMPaxePower = 0;
        TEMPdiggingPower = 0;
        TEMPreloadTime = 0;

        gameObject.SetActive(false);
    }

    public GameObject FindClosestSlotNumber()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("UIItemSlot");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
