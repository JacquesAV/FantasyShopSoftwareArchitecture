using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Derived from shop view, it specializes itself with a grid style view
public class ShopListView : ShopView
{
    [SerializeField]
    private VerticalLayoutGroup itemLayoutGroup=null; //Links to a VerticalLayoutGroup in the Unity scene
    
    [SerializeField]
    protected private GameObject infoPanel; //Links to a info panel in the Unity scene

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

        ListViewItemContainer itemContainer = newItemIcon.GetComponent<ListViewItemContainer>();
        itemContainer.infoPanelDataHolder = infoPanel.GetComponent<InfoPanelDataHolder>(); //Set reference to the relevant info panel
        Debug.Assert(itemContainer != null);
        bool isSelected = (item == shopModel.GetSelectedItem());
        itemContainer.Initialize(item, isSelected);
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  SwitchToKeyboardControl()
    //------------------------------------------------------------------------------------------------------------------------    
    protected override void SwitchToKeyboardControl()
    {
        base.SwitchToKeyboardControl(); //Calls the base method which prepares for the new controller
        shopController = gameObject.AddComponent<ListViewKeyboardController>().Initialize(shopModel);//Create and add a keyboard controller
    }
}
