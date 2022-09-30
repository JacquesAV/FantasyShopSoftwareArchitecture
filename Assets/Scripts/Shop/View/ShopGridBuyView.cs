using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This class connects a grid view for buy state of the shop to a controller to manipulate the BuyModel via a ShopController
/// interface, it contains specific methods to setup and update a grid view, with the data from a BuyModel. If you want to display
/// informationoutside of the BuyModel, for example, the money amount from the player's inventory, then you need to either keep a
/// reference to all the related models, or make this class an observer/event subscriber of the related models.
/// </summary>

//Inherits all protected functions from ShopGridView which allows for primary functionality
//This derived class extends into the specific functionality of the player buying from a store
public class ShopGridBuyView : ShopGridView
{
    [SerializeField]
    private Button restockButton = null;

    //Event related methods that help with inventory retrieval
    private ShopBuyEventMethod buyEventMethod;

    public void OnEnable()
    {
        Debug.Log("ShopGridBuyView has been selected/enabled!");
    }

    protected private void Start()
    {
        //Initialize the buy functionality
        shopModel = new BuyModel();

        //Initialize important event based methods that help retrieve the correct inventory
        buyEventMethod = this.gameObject.AddComponent<ShopBuyEventMethod>();
        buyEventMethod.shopModel = shopModel;

        //Initialize the view
        InitializeView();
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  InitializeButtons()
    //------------------------------------------------------------------------------------------------------------------------        
    //This method adds a listener to the 'Buy' button.They are forwarded to the controller.Since this is the confirm button of
    //the buy view, it will just call the controller interface's ConfirmSelectedItem function, the controller will handle the rest.
    override public void InitializeButtons()
    {
        //Call the base classes intended function
        base.InitializeButtons();

        //Extend with new button functionality
        restockButton.onClick.AddListener(
            delegate
            {
                shopController.RestockInventory(shopModel.shopInventory.initialItemCount, true);
            }
        );
    }
}
