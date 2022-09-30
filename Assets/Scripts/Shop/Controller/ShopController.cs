using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class defines the methods to be called by views to control a ShopModel. You can make concrete
/// controllers like a mouse controller, keyboard controller, gamepad controller, etc from this interface.
/// </summary>
public abstract class ShopController : MonoBehaviour
{ 
    public ShopModel Model => model;//Public getter for the model
    protected ShopModel model; //Ties this controller to a ShopModel

    protected private int currentItemIndex = 0;//The current item index is changed whenever the focus is moved with keyboard keys

    public abstract void HandleInput(); //Concrete controllers override this method and handle input in different ways.

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Initialize()
    //------------------------------------------------------------------------------------------------------------------------   
    //Used as the equivalence of a constructor since we can't use new to create a MonoBehaviour, marked as virtual so that
    //concrete controllers can add their own Initialize methods
    public virtual ShopController Initialize(ShopModel pModel)
    {
        model = pModel; //Set the current or given model
        currentItemIndex = model.GetSelectedItemIndex();//Synchronize the current item index with the model
        return this;
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  SelectItem()
    //------------------------------------------------------------------------------------------------------------------------ 
    //Called when a certain item is selected
    public void SelectItem(Item item)
    { 
        Debug.Log(item.currentRarity+ " " + item.basicData.itemName + " was selected!");
        model.SelectItem(item);
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  SelectItemByIndex()
    //------------------------------------------------------------------------------------------------------------------------ 
    //Select an item by its index
    public void SelectItemByIndex(int index)
    {
        model.SelectItemByIndex(index);
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  ConfirmSelectedItem()
    //------------------------------------------------------------------------------------------------------------------------ 
    //Tells the model to confirm the current selected item
    public void ConfirmSelectedItem()
    {
        model.ConfirmSelectedItem();
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Restock()
    //------------------------------------------------------------------------------------------------------------------------ 
    //Tells the model to confirm the current selected item
    public void RestockInventory(int pItemCount, bool isSoldByStore)
    {
        model.StockInventory(pItemCount, isSoldByStore);
    }
}
