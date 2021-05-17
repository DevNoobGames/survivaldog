using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{
    public List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();

        addItem(new Item { itemType = Item.ItemType.Wood, amount = 2 });   
        addItem(new Item { itemType = Item.ItemType.Stone, amount = 5 });   
    }

    public void addItem(Item item)
    {
        itemList.Add(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
