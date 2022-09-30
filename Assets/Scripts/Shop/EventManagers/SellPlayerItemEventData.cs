using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Event that informs subscribers that a specific item was sold by the player
public class SellPlayerItemEventData : EventData
{
    public readonly Item item;

    public SellPlayerItemEventData(Item givenItem) : base(EventType.SellPlayerItem)
    {
        item = givenItem;
    }
}
