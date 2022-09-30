using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Event that informs subscribers of the active store inventory
//Providing a reference so that one inventory is accessed across multiple objects
public class ActiveStoreInventoryEventData : EventData
{
    public readonly Inventory storeInventory;
    public ActiveStoreInventoryEventData(ref Inventory givenInventory) : base(EventType.ActiveStoreInventory)
    {
        storeInventory = givenInventory;
    }
}