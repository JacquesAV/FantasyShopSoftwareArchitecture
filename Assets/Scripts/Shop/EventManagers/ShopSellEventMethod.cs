using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is dedicated to managing and granting the event handling related functions that a sell store needs
//These are intended to allow the selling method of a shop to fire off inventory requests and to recieve them for model updating
public class ShopSellEventMethod : MonoBehaviour
{
    public ShopModel shopModel; //Model that gets referenced to update inventory

    //Constructor for it to be built with the correct and relevant information
    public ShopSellEventMethod(ShopModel shopModel)
    {
        this.shopModel = shopModel;
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Event handling
    //------------------------------------------------------------------------------------------------------------------------
    void OnEnable()
    {
        //Subscribes the method and event type to the current manager
        EventManager.currentManager.Subscribe(EventType.ActivePlayerInventory, OnRecievePlayerInventory);
    }
    void OnDisable()
    {
        //Subscribes the method and event type to the current manager
        EventManager.currentManager.Unsubscribe(EventType.ActivePlayerInventory, OnRecievePlayerInventory);
    }

    private void Start()
    {
        //Fires off request for the players current inventory
        EventManager.currentManager.AddEvent(new RequestPlayerInventoryEventData());
    }

    ////------------------------------------------------------------------------------------------------------------------------
    ////                                                  OnRecievePlayerInventory(EventData eventData)
    ////------------------------------------------------------------------------------------------------------------------------ 
    //Gets called through the eventqueue system
    private void OnRecievePlayerInventory(EventData eventData)
    {
        //Cast and error handling to make sure that the correct type of EventData is being recieved
        if (eventData is ActivePlayerInventoryEventData inventoryEventData)
        {
            Debug.Log("Player inventory reference recieved!");
            shopModel.shopInventory = inventoryEventData.playerInventory;
        }
        else
        {
            //Throw an error (Log file)
            System.Console.WriteLine("Warning: Given EventData is not the same as the permitted ActivePlayerInventoryEventData");

            //Unity Player
            Debug.Log("Warning: Given EventData is not the same as the permitted ActivePlayerInventoryEventData");
        }
    }
}
