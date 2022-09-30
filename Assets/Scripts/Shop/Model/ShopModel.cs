using System;
using System.Collections.Generic;
using UnityEngine;

//Generic/Base class for the shops model, it should not be used on its own as it lacks specific functionality
//that gets defined by derived classes
//Will be used in defining specific types/states of a store, such as buy, sell or upgrade in list and grid forms
public abstract class ShopModel
{
    public Inventory shopInventory { set; get; } = null; // Getter of the inventory, the views might need this to set up the display.
    
    protected int selectedItemIndex = 0; //selected item index

    //Used for a shops that do not recieve parameters like money or item count
    //Starts it off with an empty inventory to allow for correct view initializations
    public ShopModel()
    {
        //Initializes with an empty inventory as a placeholder for when called
        shopInventory = new Inventory();
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  GetSelectedItem()
    //------------------------------------------------------------------------------------------------------------------------        
    //Returns the selected item, has bool to prevent console spam about warning
    public Item GetSelectedItem()
    {
        if (selectedItemIndex >= 0 && selectedItemIndex < shopInventory.GetItemCount())
        {
            return shopInventory.GetItemByIndex(selectedItemIndex);
        }
        else
        {
            //Log error
            Debug.LogWarning("Index of selected item was negative or out of range!");
            return null;
        }
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  SelectItemByIndex()
    //------------------------------------------------------------------------------------------------------------------------        
    //Attempts to select the item, specified by 'index', fails with out of range exception
    public void SelectItemByIndex(int index)
    {
        if (index >= 0 && index < shopInventory.GetItemCount())
        {
            selectedItemIndex = index;
        }
        else
        {
            //Log and throw the out of range exception if failing
            Debug.LogWarning("Index of selected item was negative or out of range!");
            throw new ArgumentOutOfRangeException();
        }
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  SelectItem(Item item)
    //------------------------------------------------------------------------------------------------------------------------
    //Attempts to select the given item, fails with null or out of range exceptions
    public void SelectItem(Item item) //Why using a null check? Because null implies nothing is selected
    {
        if (item != null)
        {
           
            int index = shopInventory.GetItems().IndexOf(item);
            if (index >= 0)
            {
                selectedItemIndex = index;
            }
            else
            {
                //Log and throw the out of range exception if failing
                Debug.LogWarning("Index of selected item was negative or out of range!");
                throw new ArgumentOutOfRangeException();
            }
        }
        else
        {
            //Log and throw the null exception
            Debug.LogWarning("Item selected was null!");
            throw new ArgumentNullException();
        }
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  GetSelectedItemIndex()
    //------------------------------------------------------------------------------------------------------------------------
    //returns the index of the current selected item
    public int GetSelectedItemIndex()
    {
        return selectedItemIndex;
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Confirm()
    //------------------------------------------------------------------------------------------------------------------------        
    //Concrete classes to implement 
    public abstract void ConfirmSelectedItem();

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  RemoveSelectedItem()
    //------------------------------------------------------------------------------------------------------------------------
    public void OnRemoveSelectedItem()
    {
        //Removes item from ShopModel
        shopInventory.RemoveItemByIndex(selectedItemIndex);
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  StockStore()
    //------------------------------------------------------------------------------------------------------------------------        
    //Attempts to restock the store based on the inputs
    public void StockInventory(int pItemCount,bool isSoldByStore)
    {
        Debug.Log("Restoking shop inventory with an "+ shopInventory.itemFactory+"!");
        shopInventory.ClearInventory();
        shopInventory.PopulateInventory(pItemCount, isSoldByStore);
    }
}

