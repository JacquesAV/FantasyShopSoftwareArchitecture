using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Inherits all protected functions from ShopListView which allows for primary functionality
//This derived class extends into the specific functionality of the player buying from a store
public class ShopListBuyView : ShopListView
{
    //Event related methods that help with inventory retrieval
    private ShopBuyEventMethod buyEventMethod;

    public void OnEnable()
    {
        Debug.Log("ShopListBuyView has been selected/enabled!");
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
}
