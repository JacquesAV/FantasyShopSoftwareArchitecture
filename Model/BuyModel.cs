using System;
using System.Configuration;

/// <summary>
/// This is a concrete, empty model for the buy state of the shop for you to implement
/// </summary>
public class BuyModel : ShopModel
{
    public BuyModel(float pPriceModifier, int pItemCount, int pMoney) : base(pPriceModifier, pItemCount, pMoney)
    {

    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                 ConfirmSelectedItem()
    //------------------------------------------------------------------------------------------------------------------------        
    //Currently it just removes the selected item from the shop's inventory, rewrite this function and don't forget the unit test.

    public override void ConfirmSelectedItem()
    {
        inventory.RemoveItemByIndex(selectedItemIndex);
    }

}
