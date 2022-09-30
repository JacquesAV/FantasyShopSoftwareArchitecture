using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Derived from shop view, it specializes itself with a grid style view
public class ShopGridView : ShopView
{
    [SerializeField]
    private GridLayoutGroup itemLayoutGroup=null; //Links to a GridLayoutGroup in the Unity scene

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Initializer
    //------------------------------------------------------------------------------------------------------------------------
    protected override void InitializeView()
    {
        base.InitializeView();
        SetupItemIconView(); //Setup the grid view's properties
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  RepopulateItems()
    //------------------------------------------------------------------------------------------------------------------------        
    //Clears the grid view and repopulates it with new icons (updates the visible icons)
    protected override void RepopulateItemIconView()
    {
        ClearIconView(itemLayoutGroup);
        PopulateItemIconView();
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  AddItemToView()
    //------------------------------------------------------------------------------------------------------------------------        
    //Adds a new item container to the view, each view can have its way of displaying items
    protected override void AddItemToView(Item item)
    {
        GameObject newItemIcon = GameObject.Instantiate(itemPrefab);
        newItemIcon.transform.SetParent(itemLayoutGroup.transform);
        newItemIcon.transform.localScale = Vector3.one;//The scale would automatically change in Unity so we set it back to Vector3.one.

        GridViewItemContainer itemContainer = newItemIcon.GetComponent<GridViewItemContainer>();
        Debug.Assert(itemContainer != null);
        bool isSelected = (item == shopModel.GetSelectedItem());
        itemContainer.Initialize(item, isSelected);
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  SetupItemIconView()
    //------------------------------------------------------------------------------------------------------------------------        
    //Setup the grid view according to the ViewConfig object's requirements, right now it just sets the constraint mode and column count,
    //you can make cosmetic adjustments to the GridLayoutGroup by adding more configurations to ViewConfig and use them adjusting properties
    //like cellSize, spacing, padding, etc.
    protected private void SetupItemIconView()
    {
        itemLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;//Set the constraint mode of the GridLayoutGroup
        itemLayoutGroup.constraintCount = viewConfig.gridViewColumnCount; //Set the column count according to the ViewConfig object
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  SwitchToKeyboardControl()
    //------------------------------------------------------------------------------------------------------------------------    
    protected override void SwitchToKeyboardControl()
    {
        base.SwitchToKeyboardControl(); //Calls the base method which prepares for the new controller
        shopController = gameObject.AddComponent<GridViewKeyboardController>().Initialize(shopModel);//Create and add a keyboard controller
    }
}
