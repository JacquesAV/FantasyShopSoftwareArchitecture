using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;


//Derived from ViewItemContainer, giving more dedicated functionality to a list view item container
public class GridViewItemContainer : ViewItemContainer
{
    //Link to the infomation panel (set in prefab)
    [SerializeField]
    private GameObject infoPanel = null;

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Initialize()
    //------------------------------------------------------------------------------------------------------------------------
    public void Initialize(Item item, bool isSelected)
    {
        //Stores the item
        this.item = item;

        //Sets the highlight image and infoPanel's visibility
        if (isSelected) 
        {
            highlightPanel.gameObject.SetActive(true);
            infoPanel.SetActive(true);
        }

        //Updates the view information based on the item
        UpdateViewInformation();

        //Updates colors of containers based on rarity
        UpdateInfoPanelColors(item.currentRarity);
        UpdateHighlightColor(item.currentRarity);
    }
}
