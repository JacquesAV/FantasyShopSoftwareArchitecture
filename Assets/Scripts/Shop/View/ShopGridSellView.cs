using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Inherits all protected functions from ShopGridView which allows for primary functionality
//This derived class extends into the specific functionality of the player selling to a store
public class ShopGridSellView : ShopGridView
{
    //Event related methods that help with inventory retrieval
    public ShopSellEventMethod sellEventMethod;

    public void OnEnable()
    {
        Debug.Log("ShopGridSellView has been selected/enabled!");
    }

    protected private void Start()
    {
        //Initialize the sell functionality
        shopModel = new SellModel();
       
        //Initialize important event based methods that help retrieve the correct inventory
        sellEventMethod = this.gameObject.AddComponent<ShopSellEventMethod>();
        sellEventMethod.shopModel = shopModel;

        InitializeView();
    }
}
