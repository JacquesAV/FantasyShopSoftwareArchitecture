using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Event that informs subscribers that a purchase is being attempted
//Should be followed up by an event firing off as successful if purchase could be made 
public class BuyBeginEventData : EventData
{
    public readonly Item item;

    public BuyBeginEventData(Item givenItem) : base(EventType.BuyBegin)
    {
        item = givenItem;
    }
}
