using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class provides a keyboard controller for a ShopModel in a grid view, it defines how to handle keyboard input in HandleInput()
/// </summary>
public class GridViewKeyboardController : ShopController
{
    private ViewConfig viewConfig;//To move the focus up and down, we need to know how many columns the grid view has, in the current setup,
    private int columnCount;      //this information can be found in a ViewConfig scriptable object, which serves as a configuration file for
                                  //views.
    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Initialize()
    //------------------------------------------------------------------------------------------------------------------------
    //Override Initialize to set up additional information needed by this concrete controller: number of columns in the view
    public override ShopController Initialize(ShopModel pShopModel)
    {
        base.Initialize(pShopModel);//Call base.Initialize to set up the model

        //View config setup
        viewConfig = Resources.Load<ViewConfig>("ViewConfig");//Load the ViewConfig scriptable object from the Resources folder
        Debug.Assert(viewConfig != null);
        columnCount = viewConfig.gridViewColumnCount;//Try to set up the column count, fails silently
        return this;
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  HandleInput()
    //------------------------------------------------------------------------------------------------------------------------
    //Currently hardcoded to AWSD to move focus and K to confirm the selected item
    public override void HandleInput()
    {
        //Move the focus to the left if possible
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentItemIndex--;
            if (currentItemIndex < 0)
            {
                currentItemIndex = 0;
            }
        }

        //Move the focus to the right if possible
        if (Input.GetKeyDown(KeyCode.D))
        {
            currentItemIndex++;
            if (currentItemIndex >= this.Model.shopInventory.GetItemCount())
            {
                currentItemIndex = this.Model.shopInventory.GetItemCount() - 1;
            }
        }

        //Move the focus up if possible
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (currentItemIndex > columnCount - 1)
                currentItemIndex -= columnCount;
        }

        //Move the focus down if possible
        if (Input.GetKeyDown(KeyCode.S))
        {
;            if (currentItemIndex < this.Model.shopInventory.GetItemCount() - columnCount)
                currentItemIndex += columnCount;
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
