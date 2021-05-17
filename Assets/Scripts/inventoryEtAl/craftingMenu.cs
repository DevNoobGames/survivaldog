using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class craftingMenu : MonoBehaviour
{
    public UIItemSlot[] itemslots;
    public UIItemSlot result;
    public inventoryItemTypes invenTypes;
    public UIItemSlotDragger dragger;

    public TextMeshProUGUI explainText;

    public List<itemsClass> itemList = new List<itemsClass>();
    public List<itemsClass> itemsToDestroy = new List<itemsClass>();

    public GameObject craftMenu;

    [System.Serializable]
    public class itemsClass
    {

        public int classID;
        public int classAmount;

        public itemsClass(int itemInList, int Amount)
        {
            classID = itemInList;
            classAmount = Amount;
        }
    }


    public void CheckNumbers()
    {


        for (int i = 0; i < itemList.Count; i++)
        {
            itemList[i].classID = itemslots[i].itemInIdList;
            itemList[i].classAmount = itemslots[i].amount;
        }

        Combinations();
    }


    public void Combinations()
    {
        dragger.ClearASlot(result);
        itemsToDestroy.Clear(); //restart fresh (Should this be at every if statement below??)

        if (itemList.Exists(x => x.classID == 1 && x.classAmount >= 30) && itemList.Exists(x => x.classID == 7 && x.classAmount >= 30) && itemList.Exists(x => x.classID == 3 && x.classAmount >= 10) && itemList.Exists(x => x.classID == 2 && x.classAmount >= 1))
        {
            itemsToDestroy.Add(new itemsClass(1, 30));
            itemsToDestroy.Add(new itemsClass(7, 30));
            itemsToDestroy.Add(new itemsClass(3, 10));
            itemsToDestroy.Add(new itemsClass(2, 1));
            Result(result, 8, 1);
            if (craftMenu.activeInHierarchy)
            {
                GameObject.FindGameObjectWithTag("craftSound").GetComponent<AudioSource>().Play();
            }
        }
        else if (itemList.Exists(x => x.classID == 7 && x.classAmount >= 1) && itemList.Exists(x => x.classID == 1 && x.classAmount >= 4))
        {
            itemsToDestroy.Add(new itemsClass(7, 1));
            itemsToDestroy.Add(new itemsClass(1, 4));
            Result(result, 5, 1);
            if (craftMenu.activeInHierarchy)
            {
                GameObject.FindGameObjectWithTag("craftSound").GetComponent<AudioSource>().Play();
            }
        }
        else if (itemList.Exists(x => x.classID == 3 && x.classAmount >=1)) //has a bone!
        {
            itemsToDestroy.Add(new itemsClass(3, 1));
            Result(result, 4, 1);
            if (craftMenu.activeInHierarchy)
            {
                GameObject.FindGameObjectWithTag("craftSound").GetComponent<AudioSource>().Play();
            }
        }
        else if (itemList.Exists(x => x.classID == 9 && x.classAmount >= 1)) 
        {
            itemsToDestroy.Add(new itemsClass(9, 1));
            Result(result, 10, 50);
            if (craftMenu.activeInHierarchy)
            {
                GameObject.FindGameObjectWithTag("craftSound").GetComponent<AudioSource>().Play();
            }
            explainText.text = "";
        }
    }

    public void Result(UIItemSlot slot, int itemInIdList, int amount)
    {
        slot.itemName = invenTypes.itemtypes[itemInIdList].itemName;
        slot.itemInIdList = itemInIdList;
        slot.correspondingSprite = invenTypes.itemtypes[itemInIdList].inventorySprite;
        slot.isTool = invenTypes.itemtypes[itemInIdList].isTool;
        slot.amount = amount;
        slot.range = invenTypes.itemtypes[itemInIdList].range;
        slot.damagePower = invenTypes.itemtypes[itemInIdList].damagePower;
        slot.choppingPower = invenTypes.itemtypes[itemInIdList].choppingPower;
        slot.axePower = invenTypes.itemtypes[itemInIdList].axePower;
        slot.diggingPower = invenTypes.itemtypes[itemInIdList].diggingPower;
        slot.reloadTime = invenTypes.itemtypes[itemInIdList].reloadTime;
        slot.isFarm = invenTypes.itemtypes[itemInIdList].isFarm;

        slot.slotAmount.text = amount.ToString();
        slot.slotAmount.enabled = true;
        slot.spriteImageObj.sprite = slot.correspondingSprite;
        slot.spriteImageObj.enabled = true;
    }
}
