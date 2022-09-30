using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

/// <summary>
/// This class is applied to a button that represents an Item in the View. It is a visual representation of the item
/// when it is visible in the store. The class holds a link to the original Item, it sets the icon of the button to
/// the one specified in the Item data, and it enables or disables the infoPanel to indicate if the item is selected
/// and display the details of the item.
/// </summary>
public class ViewItemContainer : MonoBehaviour, IItemContainer
{
    //Colors assosiated to each rarity type
    protected private static Color commonColor = new Color32(130, 130, 130, 175); //828282 (Hexadecimal)
    protected private static Color commonTextColor = new Color32(255, 255, 255, 255); //FFFFFF (Hexadecimal)

    protected private static Color uncommonColor = new Color32(15, 121, 25, 175); //0F7919 (Hexadecimal)
    protected private static Color uncommonTextColor = new Color32(32, 255, 38, 255); //20FF26 (Hexadecimal)

    protected private static Color rareColor = new Color32(15, 44, 121, 175); //0F2C79 (Hexadecimal)
    protected private static Color rareTextColor = new Color32(32, 140, 255, 255); //208CFF (Hexadecimal)

    protected private static Color unknownColor = new Color32(130, 130, 130, 175); //In case of errors or unknown rarity inputs
    protected private static Color unknownTextColor = new Color32(255, 255, 255, 255); //In case of errors or unknown rarity inputs

    [SerializeField]
    protected private Image highlightPanel = null; //Highlight for the selection

    public Item Item => item;//Public getter for the item, required by IItemContainer interface.

    //Link to the infomation panel (set by prefab from ShopListView)
    public InfoPanelDataHolder infoPanelDataHolder;

    //Link to the atlas of all the item icons, use to retrieve sprites for items. For more information of the API check:
    // https://docs.unity3d.com/2019.3/Documentation/Manual/class-SpriteAtlas.html
    [SerializeField]
    protected private SpriteAtlas iconAtlas;

    //link to the original item (set in Initialize)
    protected private Item item;

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  UpdateTextInformation()
    //------------------------------------------------------------------------------------------------------------------------
    //Updates the view of the grid item container with all the important and relevant information from the item
    protected private void UpdateViewInformation()
    {
        // Clones the first Sprite in the icon atlas that matches the iconName and uses it as the sprite of the icon image.
        Sprite sprite = iconAtlas.GetSprite(item.basicData.iconName);
        if (sprite != null)
        {
            infoPanelDataHolder.itemIcon.sprite = sprite;
        }

        //Rarity Caption Image
        UpdateCaptionColor(item.currentRarity);

        //Name
        infoPanelDataHolder.namePanel.SetText(item.basicData.itemName);

        //Category
        infoPanelDataHolder.categoryPanel.SetText(item.GetType().ToString());

        //Attributes
        infoPanelDataHolder.attributesPanel.SetText(item.basicData.propertyName.ToString() + ": " + item.rarityPropertyValue);

        //Description
        infoPanelDataHolder.descriptionPanel.SetText(item.basicData.itemDescription.ToString());

        //Rarity
        infoPanelDataHolder.rarityPanel.SetText(item.currentRarity.ToString());

        //Relevant Price
        if (item.isSoldByStore)
        {
            //If being sold by store
            infoPanelDataHolder.pricePanel.SetText(item.rarityBuyPrice.ToString());
        }
        else
        {
            infoPanelDataHolder.pricePanel.SetText(item.raritySellUpgradePrice.ToString());
        }
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  UpdateContainerColor(Item.Rarity rarity)
    //------------------------------------------------------------------------------------------------------------------------
    //Updates the colors of important item containers based on the rarity of the item
    protected private void UpdateInfoPanelColors(Rarity rarity)
    {
        if (rarity is Rarity.Common)
        {
            //Text
            infoPanelDataHolder.rarityPanel.color = commonTextColor;
        }
        else if (rarity is Rarity.Uncommon)
        {
            //Text
            infoPanelDataHolder.rarityPanel.color = uncommonTextColor;
        }
        else if (rarity is Rarity.Rare)
        {
            //Text
            infoPanelDataHolder.rarityPanel.color = rareTextColor;
        }
        //In case of unknown rarity or errors
        else
        {
            //Text
            infoPanelDataHolder.rarityPanel.color = unknownTextColor;
        }
    }
    //------------------------------------------------------------------------------------------------------------------------
    //                                                  UpdateHighlightColor(Item.Rarity rarity)
    //------------------------------------------------------------------------------------------------------------------------
    //Updates the colors of important item containers based on the rarity of the item
    protected private void UpdateHighlightColor(Rarity rarity)
    {
        if (rarity is Rarity.Common)
        {
            //Backings
            highlightPanel.color = commonColor;
        }
        else if (rarity is Rarity.Uncommon)
        {
            //Backings
            highlightPanel.color = uncommonColor;
        }
        else if (rarity is Rarity.Rare)
        {
            //Backings
            highlightPanel.color = rareColor;
        }
        //In case of unknown rarity or errors
        else
        {
            //Backings
            highlightPanel.color = unknownColor;
        }
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  UpdateCaptionColor(Item.Rarity rarity)
    //------------------------------------------------------------------------------------------------------------------------
    //Updates the colors of important item containers based on the rarity of the item
    protected private void UpdateCaptionColor(Rarity rarity)
    {
        if (rarity is Rarity.Common)
        {
            //Backings
            infoPanelDataHolder.captionPanel.color = commonColor;
        }
        else if (rarity is Rarity.Uncommon)
        {
            //Backings
            infoPanelDataHolder.captionPanel.color = uncommonColor;
        }
        else if (rarity is Rarity.Rare)
        {
            //Backings
            infoPanelDataHolder.captionPanel.color = rareColor;
        }
        //In case of unknown rarity or errors
        else
        {
            //Backings
            infoPanelDataHolder.captionPanel.color = unknownColor;
        }
    }
}
