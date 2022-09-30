using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Event that informs subscribers of the active player inventory
//Providing a reference so that one inventory is accessed across multiple objects
public class ActivePlayerInventoryEventData : EventData
{
    public readonly Inventory playerInventory;
    public ActivePlayerInventoryEventData(ref Inventory givenInventory) : base(EventType.ActivePlayerInventory)
    {
        playerInventory = givenInventory;
    }
}