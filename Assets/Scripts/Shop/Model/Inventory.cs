using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class defines a basic inventory
/// </summary>
public class Inventory
{
    public int Money { get; set; } = 0;//Getter and setter for the money, the views need it to display the amount of money.
    public int initialItemCount { get; set; }
    public ItemFactory itemFactory; //Abstract item factory assosiated with the inventory
    private List<Item> itemList = new List<Item>(); //Items in the inventory

    //Set up the inventory with item count and money
    public Inventory(int pItemCount, int pMoney)
    {
        PopulateInventory(pItemCount,true);
        Money = pMoney;
    }
    //Populate based on item factory and money
    public Inventory(ItemFactory pItemFactory, int pMoney)
    {
        itemFactory = pItemFactory;
        Money = pMoney;
    }
    //Populate based on item factory
    public Inventory(ItemFactory pItemFactory)
    {
        itemFactory = pItemFactory;
    }
    //Construct up a placeholder inventory
    public Inventory(){}

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  GetItems()
    //------------------------------------------------------------------------------------------------------------------------        
    //Returns a list with all current items in the shop.
    public List<Item> GetItems()
    {
        return new List<Item>(itemList); //Returns a copy of the list, so the original is kept intact, 
                                         //however this is shallow copy of the original list, so changes in 
                                         //the original list will likely influence the copy, apply 
                                         //creational patterns like prototype to fix this. 
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  GetItemCount()
    //------------------------------------------------------------------------------------------------------------------------        
    //Returns the number of items
    public int GetItemCount()
    {
        return itemList.Count;
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  GetItemByIndex()
    //------------------------------------------------------------------------------------------------------------------------        
    //Attempts to get an item, specified by index, returns null if unsuccessful. Depends on how you set up your shop, it might be
    //a good idea to return a copy of the original item.
    public Item GetItemByIndex(int index)
    {
        if (index >= 0 && index < itemList.Count)
        {
            return itemList[index];
        }
        else
        {
            Debug.LogWarning("No item gotten from index!");
            return null;
        }
    }
    //------------------------------------------------------------------------------------------------------------------------
    //                                                 AddItem()
    //------------------------------------------------------------------------------------------------------------------------        
    //Adds an item to the inventory's item list.
    public void AddItem(Item item)
    {
        itemList.Add(item);//In your setup, what would happen if you add an item that's already existed in the list?
    }
    //Adds an item with isStoreItem in mind
    public void AddItem(Item item, bool isStoreItem)
    {
        item.isSoldByStore = isStoreItem;
        AddItem(item);
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                 RemoveItem()
    //------------------------------------------------------------------------------------------------------------------------        
    //Attempts to remove an item, fails silently.
    public void Remove(Item item)
    {
        if (itemList.Contains(item))
        {
            itemList.Remove(item);
        }
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                 RemoveItemByIndex()
    //------------------------------------------------------------------------------------------------------------------------        
    //Attempts to remove an item, specified by index, fails silently.
    public void RemoveItemByIndex(int index)
    {
        if (index >= 0 && index < itemList.Count)
        {
            itemList.RemoveAt(index);
        }
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  PopulateInventory() with Abstract Factory
    //------------------------------------------------------------------------------------------------------------------------
    public void PopulateInventory(int itemCount, bool isSoldByStore)
    {
        initialItemCount = itemCount;
        for (int index = 0; index < itemCount; index++)
        {
            //Gets random value for item generation
            switch (UnityEngine.Random.Range(0, 3)) //Exclusive, so 0-2)
            {
                case 0:
                    //Creates weapon using the specified/given factory and then adds to the inventory
                    Weapon weapon = itemFactory.CreateWeapon();
                    itemList.Add(weapon);
                    break;
                case 1:
                    //Creates armor using the specified/given factory and then adds to the inventory
                    Armor armor = itemFactory.CreateArmor();
                    itemList.Add(armor);
                    break;
                case 2:
                    //Creates potion using the specified/given factory and then adds to the inventory
                    Potion potion = itemFactory.CreatePotion();
                    itemList.Add(potion);
                    break;
                default:
                    //In case of error, make no item and log an error
                    Debug.LogWarning("Error with populating inventory, item not created, did you use the wrong random range?");
                    break;
            }
        }

        //Sets the current state of the items to the relevant buy/sell state
        foreach(Item item in itemList)
        {
            item.isSoldByStore = isSoldByStore;
        }

        Debug.Log("Inventory populated with " + GetItemCount() + " items, using " + itemFactory.GetType().ToString());
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  CanAffordBuy(int givenPrice)
    //------------------------------------------------------------------------------------------------------------------------
    //Checks if the inventory has a sufficient amount of money to make the givenPrice purchase
    public bool CanAffordBuy(int givenPrice)
    {
        //If price is less or equal to available money
        if (givenPrice <= Money)
        {
            return true;
        }
        else
        {
            Debug.Log("Could not afford gold cost of "+givenPrice+"!");
            return false;
        }
    }
    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Subtract/AddMoney(int ...)
    //------------------------------------------------------------------------------------------------------------------------
    //Allows for the money that each inventory contains to either be increased or decreased
    //Subtracts money to inventory
    public void SubtractMoney(int subtractedMoney)
    {
        Money -= subtractedMoney;
        Debug.Log(subtractedMoney + " gold being subtracted from inventory, "+ Money + " gold remains!");
    }

    //Adds money to inventory
    public void AddMoney(int addedMoney)
    {
        Money += addedMoney;
        Debug.Log(addedMoney + " gold being added to inventory, " + Money + " gold remains!");
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                ClearInventory()
    //------------------------------------------------------------------------------------------------------------------------        
    //Clears the inventory of all items
    public void ClearInventory()
    {
        itemList.Clear();
    }
}
