using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Event that informs subscribers that a purchase was succesfull
//Is a follow up to the BuyBeginEventData event
public class BuySuccessfulEventData : EventData
{
    public readonly Item item;
    public BuySuccessfulEventData(Item givenItem) : base(EventType.BuySuccessful)
    {
        item = givenItem;
    }
}