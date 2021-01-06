using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

/// <summary>
/// This class provides a mouse controller for a ShopModel, it receives UI events such as button clicks from the view and
/// munipluates the model it controls. It implements UnityEngine.EventSystems's IPointerClickerHandler to receive mouse click
/// events
/// </summary>
public class MouseController : ShopController, IPointerClickHandler 
{
    private Item itemToSelect;//The item that will be selected the next time HandleInput is called

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  HandleInput()
    //------------------------------------------------------------------------------------------------------------------------        
    //If there is an item to select, call SelectItem to select it. 
    public override void HandleInput()
    {
        if (itemToSelect != null)
        {
            SelectItem(itemToSelect);
            itemToSelect = null;//Now that the item was selected, set itemToSelect back to null
        }
    }


    //------------------------------------------------------------------------------------------------------------------------
    //                                                  OnPointerClick()
    //------------------------------------------------------------------------------------------------------------------------ 
    //This method is called everytime the view receives a mouse click event
    public void OnPointerClick(PointerEventData eventData)
    {
        //Check if the mouse clicked on an item container. 
        IItemContainer itemContainer = eventData.pointerCurrentRaycast.gameObject.GetComponent<IItemContainer>();

        //If the game object that was clicked on has a component which implements the IItemContainer interface, itemContainer
        //will be assigned with that component, otherwise itemContainer would be null, meaning the mouse didn't click on any
        //item containers in the view. If you are clicking on the item container but it still returns null, check to see if there
        //is any other UI elements blocking the raycast, if so mark their 'Raycast Target' property as false in Unity Editor
        if (itemContainer != null)
        {
            itemToSelect = itemContainer.Item;//Use IItemContainer's getter to get the item in the container, assign it to itemToSelect,
                                              //now it will be selected the next time HandleInput is called
                                                
        }
    }
}
