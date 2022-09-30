using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Is fired off for when an object needs to access the players inventory
//Ideally, this will cause another event to fire off at the subscriber, sending and fulfilling the request
public class RequestPlayerInventoryEventData : EventData
{
    public RequestPlayerInventoryEventData() : base(EventType.RequestPlayerInventory){}
}
