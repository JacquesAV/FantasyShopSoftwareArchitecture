using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Defines the different event types to be used in event data
//Descriptions for each available in the Derived classes of EventData
public enum EventType 
{
    BuyBegin=0,
    BuySuccessful,
    PlayerMoneyChanged,
    RequestPlayerMoney,
    RequestStoreInventory,
    ActiveStoreInventory,
    RequestPlayerInventory,
    ActivePlayerInventory,
    SellPlayerItem,
}
