using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Event that requests the current amount of money that the player inventory has
public class RequestPlayerMoneyEventData : EventData
{
    public RequestPlayerMoneyEventData() : base(EventType.RequestPlayerMoney) { }
}
