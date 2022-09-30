using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

//Serves as a script that allows for shop views to link to one another using events
//Event system
public class ShopModelManager : GenericModelManager
{
    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Event handling and start functions
    //------------------------------------------------------------------------------------------------------------------------
    void OnEnable()
    {
        //Subscribes the method and event type to the current manager
        EventManager.currentManager.Subscribe(EventType.SellPlayerItem, OnRecieveItem);
        EventManager.currentManager.Subscribe(EventType.BuySuccessful, OnSuccessfulBuy);

        EventManager.currentManager.Subscribe(EventType.RequestStoreInventory, OnInventoryReferenceRequest);
    }
    void OnDisable()
    {
        //Subscribes the method and event type to the current manager
        EventManager.currentManager.Unsubscribe(EventType.SellPlayerItem, OnRecieveItem);
        EventManager.currentManager.Unsubscribe(EventType.BuySuccessful, OnSuccessfulBuy);

        EventManager.currentManager.Unsubscribe(EventType.RequestStoreInventory, OnInventoryReferenceRequest);
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  OnRecieveItem(EventData eventData)
    //------------------------------------------------------------------------------------------------------------------------
    //When an item tries to be bought, recieve the relevant information about the purchase
    private void OnRecieveItem(EventData eventData)
    {
        //Cast and error handling to make sure that the correct type of EventData is being recieved
        if (eventData is SellPlayerItemEventData recievedEventData)
        {
            Debug.Log("Player sold  " + recievedEventData.item.basicData.itemName + " to the store for " + recievedEventData.item.raritySellUpgradePrice + " gold!");

            //Adds the item back to the store
            modelInventory.AddItem(recievedEventData.item,true);
        }
        else
        {
            //Throw an error (Log file)
            Console.WriteLine("Warning: Given EventData is not the same as the permitted SellPlayerItemEventData");

            //Unity Player
            Debug.LogWarning("Warning: Given EventData is not the same as the permitted SellPlayerItemEventData");
        }
    }
    //------------------------------------------------------------------------------------------------------------------------
    //                                                  OnInventoryReferenceRequest()
    //------------------------------------------------------------------------------------------------------------------------
    //When an object requests the stores inventory
    private void OnInventoryReferenceRequest(EventData eventData)
    {
        //Fire off an event with a reference of the players active inventory
        EventManager.currentManager.AddEvent(new ActiveStoreInventoryEventData(ref modelInventory));
    }
    //------------------------------------------------------------------------------------------------------------------------
    //                                                  OnSuccessfulBuy()
    //------------------------------------------------------------------------------------------------------------------------ 
    //Gets called through the eventqueue system
    private void OnSuccessfulBuy(EventData eventData)
    {
        //Cast and error handling to make sure that the correct type of EventData is being recieved
        if (eventData is BuySuccessfulEventData buyEventData)
        {
            modelInventory.Remove(buyEventData.item);
        }
        else
        {
            //Throw an error (Log file)
            System.Console.WriteLine("Warning: Given EventData is not the same as the permitted BuySuccessfulEventData");

            //Unity Player
            Debug.Log("Warning: Given EventData is not the same as the permitted BuySuccessfulEventData");
        }
    }
}
