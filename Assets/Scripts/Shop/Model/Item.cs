/// <summary>
/// This class holds data for an Item.
/// </summary>
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    //Allows for items to access a facade that can recieve item data for making new items
    public static PremadeItemsFacade facadeForFactory = new PremadeItemsFacade(); //Gets constructed once (static)

    //Item stats that define information about the item
    public readonly BasicItemData basicData;

    // This is the base property for the item
    public int rarityPropertyValue;

    //Buy Price for the store (from store inv)
    public int rarityBuyPrice { get; private set; }

    //Sell Price for the player (from player inv)
    public int raritySellUpgradePrice { get; private set; }

    //Enum that allow for item containers to know if item is being bought
    public bool isSoldByStore { get; set; } = false;

    //Enums that allow for items to have rarities, which affects buy and sell price
    public Rarity currentRarity;

    //The return price or sell ratio (Ratio for the items current price [based on rarity] when being sold)
    private static float sellRatio = 0.5f;

    //Price modifiers for rarity
    private static float uncommonCostModifier = 1.5f;
    private static float rareCostModifier = 2f;

    //Property Value modifiers for rarity
    private static float uncommonPropertyModifier = 1.5f;
    private static float rarePropertyModifier = 2f;

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Item()
    //------------------------------------------------------------------------------------------------------------------------
    //Constructor that uses the BasicItemData structure
    public Item(BasicItemData pBasicData, Rarity pRarity)
    {
        //Allows for items and all derived classes to instantiate with the correct values
        this.basicData = pBasicData;

        this.currentRarity = pRarity;

        //Updates the items pricing based on their rarity
        UpdateRarityPricing();

        //Update property values based on their rarity
        UpdateRarityPropertyValues();
    }

    //Updates pricing for selling (from player to store) or buying (from store to player) items based on rarity
    public void UpdateRarityPricing()
    {
        //Common
        if(currentRarity==Rarity.Common)
        {
            rarityBuyPrice = basicData.basePrice;
            raritySellUpgradePrice = (int)(basicData.basePrice *sellRatio);
        }
        //Uncommon
        else if (currentRarity == Rarity.Uncommon)
        {
            rarityBuyPrice = (int)(basicData.basePrice * uncommonCostModifier);
            raritySellUpgradePrice = (int)((basicData.basePrice * uncommonCostModifier) * sellRatio);
        }
        //Rare
        else
        {
            rarityBuyPrice = (int)(basicData.basePrice * rareCostModifier);
            raritySellUpgradePrice = (int)((basicData.basePrice * rareCostModifier) * sellRatio);
        }
    }
    //Updates property values based on rarity
    public void UpdateRarityPropertyValues()
    {
        //Common
        if (currentRarity == Rarity.Common)
        {
            rarityPropertyValue = basicData.basePropertyValue;
        }
        //Uncommon
        else if (currentRarity == Rarity.Uncommon)
        {
            rarityPropertyValue = (int)(basicData.basePropertyValue * uncommonPropertyModifier);
        }
        //Rare
        else
        {
            rarityPropertyValue = (int)(basicData.basePropertyValue * rarePropertyModifier);
        }
    }

    //Upgrades the item to a higher rarity
    public void Upgrade()
    {
        Debug.Log("Upgraded  " + basicData.itemName + " from " + currentRarity + " to " + (currentRarity + 1));

        //Increases the rarity by 1
        currentRarity = currentRarity + 1;

        //Updates the pricing and property values based on the new rarity
        UpdateRarityPricing();
        UpdateRarityPropertyValues();
    }
}

