using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

//Derived from ViewItemContainer, giving more dedicated functionality to a list view item container
public class ListViewItemContainer : ViewItemContainer
{
    //The item panels primary information, this is constantly displayed compared to its grid item counterpart
    [SerializeField]
    private TMPro.TextMeshProUGUI namePanel = null, pricePanel = null, categoryPanel = null, rarityPanel = null;

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Initialize()
    //------------------------------------------------------------------------------------------------------------------------
    public void Initialize(Item item, bool isSelected)
    {
        //Stores the item
        this.item = item;

        //Sets the highlight toggle and infoPanel's updated info based on the item
        if (isSelected)
        {
            //Activate the highlight panel
            highlightPanel.gameObject.SetActive(true);

            //Updates highlights
            UpdateHighlightColor(item.currentRarity);

            //Updates colors of containers based on rarity
            UpdateInfoPanelColors(item.currentRarity);

            //Update the view information
            UpdateViewInformation();
        }

        //Update the information that is displayed about the item
        UpdateListInformation();
    }
    //------------------------------------------------------------------------------------------------------------------------
    //                                                  UpdateListInformation()
    //------------------------------------------------------------------------------------------------------------------------
    //Updates the simple list information of the item
    private void UpdateListInformation()
    {
        //Update name and rarity panel
        namePanel.SetText(item.basicData.itemName);
        categoryPanel.SetText(item.GetType().ToString());
        rarityPanel.SetText(item.currentRarity.ToString());

        //Relevant Price
        if (item.isSoldByStore)
        {
            //If being sold by store
            pricePanel.SetText(item.rarityBuyPrice.ToString());
        }
        else
        {
            //If being sold or upgraded by user
            pricePanel.SetText(item.raritySellUpgradePrice.ToString());
        }
    }
}
