using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class provides a keyboard controller for a ShopModel in a list view, it defines how to handle keyboard input in HandleInput()
/// </summary>
public class ListViewKeyboardController : ShopController
{
    //------------------------------------------------------------------------------------------------------------------------
    //                                                  HandleInput()
    //------------------------------------------------------------------------------------------------------------------------
    //Currently hardcoded to AWSD to move focus and K to confirm the selected item
    public override void HandleInput()
    {
        //Move the focus to the left/up if possible
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W))
        {
            currentItemIndex--;
            if (currentItemIndex < 0)
            {
                currentItemIndex = 0;
            }
        }

        //Move the focus to the right/down if possible
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S))
        {
            currentItemIndex++;
            if (currentItemIndex >= this.Model.shopInventory.GetItemCount())
            {
                currentItemIndex = this.Model.shopInventory.GetItemCount() - 1;
            }
        }

        //Select the item
        SelectItemByIndex(currentItemIndex);

        //Confirm the selected item when K is pressed
        if (Input.GetKeyDown(KeyCode.K))
        {
            ConfirmSelectedItem();
        }
    }
 }