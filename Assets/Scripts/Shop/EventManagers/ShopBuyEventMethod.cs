using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is dedicated to managing and granting the event handling related functions that a buy store needs
//These are intended to allow the buying method of a shop to fire off inventory requests and to recieve them for model updating
public class ShopBuyEventMethod : MonoBehaviour
{
    public ShopModel shopModel; //Model that gets referenced to update inventory

    //Constructor for it to be built with the correct and relevant information
    public ShopBuyEventMethod(ShopModel shopModel)
    {
        this.shopModel = shopModel;
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Event handling
    //------------------------------------------------------------------------------------------------------------------------
    void OnEnable()
    {
        //Subscribes the method and event type to the current manager
        EventManager.currentManager.Subscribe(EventType.ActiveStoreInventory, OnRecieveStoreInventory);
    }
    void OnDisable()
    {
        //Subscribes the method and event type to the current manager
        EventManager.currentManager.Unsubscribe(EventType.ActiveStoreInventory, OnRecieveStoreInventory);
    }

    private void Start()
    {
        //Fires off request for the stores current inventory
        EventManager.currentManager.AddEvent(new RequestStoreInventoryEventData());
    }

    ////------------------------------------------------------------------------------------------------------------------------
    ////                                                  OnRecieveStoreInventory(EventData eventData)
    ////------------------------------------------------------------------------------------------------------------------------ 
    //Gets called through the eventqueue system, recieves the active store
    private void OnRecieveStoreInventory(EventData eventData)
    {
        //Cast and error handling to make sure that the correct type of EventData is being recieved
        if (eventData is ActiveStoreInventoryEventData itemEventData)
        {
            Debug.Log("Shop inventory reference recieved!");
            shopModel.shopInventory = itemEventData.storeInventory;
        }
        else
        {
            //Throw an error (Log file)
            System.Console.WriteLine("Warning: Given EventData is not the same as the permitted ActiveStoreInventoryEventData");

            //Unity Player
            Debug.Log("Warning: Given EventData is not the same as the permitted ActiveStoreInventoryEventData");
        }
    }
}
