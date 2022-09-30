using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The selling functionality that the shop is meant to have
//Deals specifically with what happens after an item is confirmed
public class SellModel : ShopModel
{
    //Constructor
    public SellModel() : base(){}

    //------------------------------------------------------------------------------------------------------------------------
    //                                                 ConfirmSelectedItem()
    //------------------------------------------------------------------------------------------------------------------------        
    public override void ConfirmSelectedItem()
    {
        SellItem();
    }
    private void SellItem()
    {
        //Fire off event with the confirmed item trying to be sold back to the store
        //This will add an event to the event queue
        if (shopInventory.GetItemByIndex(selectedItemIndex) != null)
        {
            EventManager.currentManager.AddEvent(
                new SellPlayerItemEventData(
                shopInventory.GetItemByIndex(selectedItemIndex)));

            //As no confirmation is needed between the sellModel and the store inventory, transaction is immediate
            //Add money to the references inventory
            shopInventory.AddMoney(shopInventory.GetItemByIndex(selectedItemIndex).raritySellUpgradePrice);

            //Remove item
            shopInventory.RemoveItemByIndex(selectedItemIndex);

            //Inform subscribers of player inventory money change
            EventManager.currentManager.AddEvent(new PlayerMoneyChangedEventData(shopInventory.Money));
        }
    }
}