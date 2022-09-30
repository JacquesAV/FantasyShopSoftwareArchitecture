using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Event that provides the amount of player money remaining to all relevant subscribers, such as the money display
public class PlayerMoneyChangedEventData : EventData
{
    public readonly int newMoney;
    public PlayerMoneyChangedEventData(int givenMoney) : base(EventType.PlayerMoneyChanged)
    {
        newMoney = givenMoney;
    }
}
