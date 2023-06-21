 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryP1
{
    public event EventHandler OnItemListChanged;
    public List<Item> itemList;

    public InventoryP1() 
    { 
        itemList = new List<Item>();
    }

    public void AddItem(Item item)
    {
        if(item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory) 
            {
                itemList.Add(item);
            }
            } else {  
            itemList.Add(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    //public void RemoveItem(Item item)
    //{
    //    if (item.IsStackable())
    //    {
    //        Item itemInInventory = null;
    //        foreach (Item inventoryItem in itemList)
    //        {
    //            if (inventoryItem.itemType == item.itemType)
    //            {
    //                inventoryItem.amount -= item.amount;
    //                itemInInventory = inventoryItem;
    //            }
    //        }
    //        if (itemInInventory != null && itemInInventory.amount <= 0)
    //        {
    //            itemList.Remove(itemInInventory);
    //        }
    //    }
    //    else
    //    {
    //        itemList.Remove(item);
    //    }
    //    OnItemListChanged?.Invoke(this, EventArgs.Empty);
    //}
    public List<Item> GetItemList()
    {
        return itemList;
    }
}
