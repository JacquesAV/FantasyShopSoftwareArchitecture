using System;
using System.Configuration;
using UnityEngine;

//The buying functionality that the shop is meant to have
//Deals specifically with what happens after an item is confirmed
public class BuyModel : ShopModel
{
    public BuyModel() : base() { }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                 ConfirmSelectedItem()
    //------------------------------------------------------------------------------------------------------------------------        
    public override void ConfirmSelectedItem()
    {
        BuyItem();
    }
    private void BuyItem()
    {
        //Fire off event with the confirmed item trying to be bought and its buying price
        //This will add an event to the event queue
        if (shopInventory.GetItemByIndex(selectedItemIndex) != null)
        {
            //Fire off event about purchase attempt
            EventManager.currentManager.AddEvent(
                new BuyBeginEventData(
                shopInventory.GetItemByIndex(selectedItemIndex)));
        }
    }
}
