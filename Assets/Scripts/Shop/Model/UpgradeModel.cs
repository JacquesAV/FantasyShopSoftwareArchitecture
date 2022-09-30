using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The upgrading functionality that the shop is meant to have
//Deals specifically with what happens after an item is confirmed
public class UpgradeModel : ShopModel
{
    //Constructor
    public UpgradeModel() : base() { }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                 ConfirmSelectedItem()
    //------------------------------------------------------------------------------------------------------------------------        
    public override void ConfirmSelectedItem()
    { 
        UpgradeItem();
    }
    private void UpgradeItem()
    {
        //If item is null, then do not execute further checks
        if(shopInventory.GetItemByIndex(selectedItemIndex) == null) {  return;  }

        //If item is already maximum rarity, then do not execute upgrade process
        if(shopInventory.GetItemByIndex(selectedItemIndex).currentRarity==Rarity.Rare)
        {
            Debug.Log(shopInventory.GetItemByIndex(selectedItemIndex).basicData.itemName + "is already at maximum rarity!");
            return;
        }

        //If user can afford to upgrade
        int upgradeCost = (int)(shopInventory.GetItemByIndex(selectedItemIndex).raritySellUpgradePrice);
        if (shopInventory.CanAffordBuy(upgradeCost))
        {
            //Remove cost from money
            shopInventory.SubtractMoney(upgradeCost);

            //Upgrade
            shopInventory.GetItemByIndex(selectedItemIndex).Upgrade();

            //Debug
            Debug.Log("Player upgraded " + shopInventory.GetItemByIndex(selectedItemIndex).basicData.itemName + " to rarity " + shopInventory.GetItemByIndex(selectedItemIndex).currentRarity + " for " + upgradeCost + " gold!");

            //Inform subscribers of player inventory money changes
            EventManager.currentManager.AddEvent(new PlayerMoneyChangedEventData(shopInventory.Money));
        }
    }
}
