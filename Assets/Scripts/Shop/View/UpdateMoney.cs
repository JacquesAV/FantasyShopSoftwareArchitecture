using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMoney : MonoBehaviour
{
    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Event handling
    //------------------------------------------------------------------------------------------------------------------------
    void OnEnable()
    {
        //Subscribes the method and event type to the current manager
        EventManager.currentManager.Subscribe(EventType.PlayerMoneyChanged, UpdateMoneyText);

        //Request current money on enabling
        EventManager.currentManager.AddEvent(new RequestPlayerMoneyEventData());
    }
    void OnDisable()
    {
        //Subscribes the method and event type to the current manager
        EventManager.currentManager.Unsubscribe(EventType.PlayerMoneyChanged, UpdateMoneyText);
    }

    //Gets called to update text
    private void UpdateMoneyText(EventData eventData)
    {
        //Cast and error handling to make sure that the correct type of EventData is being recieved
        if (eventData is PlayerMoneyChangedEventData moneyChangeEventData)
        {
            this.GetComponent<TMPro.TextMeshProUGUI>().text = moneyChangeEventData.newMoney.ToString();
        }
        else
        {
            //Throw an error (Log file)
            System.Console.WriteLine("Warning: Given EventData is not the same as the permitted PlayerMoneyChangedEventData");

            //Unity Player
            Debug.Log("Warning: Given EventData is not the same as the permitted PlayerMoneyChangedEventData");
        }
    }
}
