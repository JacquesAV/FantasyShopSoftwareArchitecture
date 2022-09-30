using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inherits all protected functions from ShopListView which allows for primary functionality
//This derived class extends into the specific functionality of the player upgrading items
public class ShopListUpgradeView : ShopListView
{
    //Event related methods that help with inventory retrieval
    public ShopUpgradeEventMethod upgradeEventMethod;

    public void OnEnable()
    {
        Debug.Log("ShopListUpgradeView has been selected/enabled!");
    }

    protected private void Start()
    {
        //Initialize the upgrade functionality
        shopModel = new UpgradeModel();

        //Initialize important event based methods that help retrieve the correct inventory
        upgradeEventMethod = this.gameObject.AddComponent<ShopUpgradeEventMethod>();
        upgradeEventMethod.shopModel = shopModel;

        InitializeView();
    }
}
