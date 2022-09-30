using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

//Event system
//Examples in this class:
//There is no direct communication in this class but rather "on item bought" in a player class it is fired with the item data
//Store asks (event) if player inventory has enough money, player sends back saying yes, store sends back to them
public class PlayerModelManager : GenericModelManager
{
    [SerializeField]
    private int startingMoney = 500;
    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Event handling and start functions
    //------------------------------------------------------------------------------------------------------------------------
    void OnEnable()
    {
        //Subscribes the method and event type to the current manager
        EventManager.currentManager.Subscribe(EventType.BuyBegin, OnBuyItem);
        EventManager.currentManager.Subscribe(EventType.RequestPlayerInventory, OnInventoryReferenceRequest);
        EventManager.currentManager.Subscribe(EventType.RequestPlayerMoney, OnFireRequestedMoney);
    }
    void OnDisable()
    {
        //Subscribes the method and event type to the current manager
        EventManager.currentManager.Unsubscribe(EventType.BuyBegin, OnBuyItem);
        EventManager.currentManager.Unsubscribe(EventType.RequestPlayerInventory, OnInventoryReferenceRequest);
        EventManager.currentManager.Unsubscribe(EventType.RequestPlayerMoney, OnFireRequestedMoney);
    }

    // Start is called before the first frame update
    override public void Start()
    {
        //Initializes inventories with relevant factory
        base.InitializeManager(false,startingMoney);

        //Fire off an event with the players newly declared amount of money
        EventManager.currentManager.AddEvent(new PlayerMoneyChangedEventData(startingMoney));
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  OnBuyItem(EventData eventData)
    //------------------------------------------------------------------------------------------------------------------------
    //When an item tries to be bought, recieve the relevant information about the purchase
    private void OnBuyItem(EventData eventData)
    {
        //Cast and error handling to make sure that the correct type of EventData is being recieved
        if (eventData is BuyBeginEventData buyEventData)
        {
            //Checks if the item is affordable
            //If affordable, then buy and fire off event for store to remove it
            if (modelInventory.CanAffordBuy(buyEventData.item.rarityBuyPrice))
            {
                //Debug
                Debug.Log("Player bought " + buyEventData.item.basicData.itemName + " from the store for " + buyEventData.item.rarityBuyPrice + " gold!");

                //Adds the new item to the player inventory
                modelInventory.AddItem(buyEventData.item,false);

                //Lowers player money
                modelInventory.SubtractMoney(buyEventData.item.rarityBuyPrice);

                //Fire off an event to say that the item was bought
                //This will add an event to the event queue
                EventManager.currentManager.AddEvent(new BuySuccessfulEventData(buyEventData.item));

                //Fire off an event with the players current amount of remaining money
                EventManager.currentManager.AddEvent(new PlayerMoneyChangedEventData(modelInventory.Money));
            }
            //If no, then do nothing
        }
        else
        {
            //Throw an error (Log file)
            Console.WriteLine("Warning: Given EventData is not the same as the permitted BuyBeginEventData");

            //Unity Player
            Debug.Log("Warning: Given EventData is not the same as the permitted BuyBeginEventData");
        }
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  OnInventoryReferenceRequest()
    //------------------------------------------------------------------------------------------------------------------------
    //When an object requests the player inventory
    private void OnInventoryReferenceRequest(EventData eventData)
    {
        //Fire off an event with a reference of the players active inventory
        EventManager.currentManager.AddEvent(new ActivePlayerInventoryEventData(ref modelInventory));
    }

    private void OnFireRequestedMoney(EventData eventData)
    {
        //Fire off the requested amount of money that the player inventory currently has
        EventManager.currentManager.AddEvent(new PlayerMoneyChangedEventData(modelInventory.Money));
    }
}
