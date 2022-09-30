using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//This class contains bare bone essentials that can be inherited by shopGridView or shopListView
//It contains the essentials that a view needs in order to adequately represent the information stored in a specific shop or inventory
//Done in order to reduce the amount of repeated code
public abstract class ShopView : MonoBehaviour
{
    public ShopModel ShopModel => shopModel; //A getter to access shopModel.

    [SerializeField]
    protected private GameObject itemPrefab; //A prefab to display an item in the view

    [SerializeField]
    protected private Button actionButton;

    [SerializeField]
    protected private TextMeshProUGUI instructionText;

    protected private ViewConfig viewConfig; //To set up the grid view, we need to know how many columns the grid view has, in the current setup,
                                             //this information can be found in a ViewConfig scriptable object, which serves as a configuration file for
                                             //views.

    protected private ShopModel shopModel; //Model in MVC pattern
    protected private ShopController shopController; //Controller in MVC pattern


    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Initializer
    //------------------------------------------------------------------------------------------------------------------------
    protected virtual void InitializeView()  //Differs depending on if it is a list or grid view
    {
        shopController = gameObject.AddComponent<MouseController>().Initialize(shopModel);//Set the default controller to be the mouse controller
        viewConfig = Resources.Load<ViewConfig>("ViewConfig");//Load the ViewConfig scriptable object from the Resources folder
        Debug.Assert(viewConfig != null);
        //SetupItemIconView(); //Setup the grid view's properties
        PopulateItemIconView(); //Display items
        InitializeButtons(); //Connect the buttons to the controller
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  InitializeButtons()
    //------------------------------------------------------------------------------------------------------------------------        
    //This method adds a listener to the 'Action' button. They are forwarded to the controller. Since this is the confirm button of
    //the buy view, it will just call the controller interface's ConfirmSelectedItem function, the controller will handle the rest.
    virtual public void InitializeButtons()
    {
        actionButton.onClick.AddListener(
            delegate {
                shopController.ConfirmSelectedItem();
            }
        );
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  RepopulateItems()
    //------------------------------------------------------------------------------------------------------------------------        
    //Clears the grid view and repopulates it with new icons (updates the visible icons)
    protected abstract void RepopulateItemIconView();

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  PopulateItems()
    //------------------------------------------------------------------------------------------------------------------------        
    //Adds one icon for each item in the shop
    protected private void PopulateItemIconView()
    {
        foreach (Item item in shopModel.shopInventory.GetItems())
        {
            AddItemToView(item);
        }
    }
    //------------------------------------------------------------------------------------------------------------------------
    //                                                  ClearIconView()
    //------------------------------------------------------------------------------------------------------------------------        
    //Removes all existing icons in the gridview
    protected private void ClearIconView(LayoutGroup itemLayoutGroup)
    {
        //Iterate over all icons and destroy them
        Transform[] allIcons = itemLayoutGroup.transform.GetComponentsInChildren<Transform>();
        foreach (Transform child in allIcons)
        {
            if (child != itemLayoutGroup.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }


    //------------------------------------------------------------------------------------------------------------------------
    //                                                  AddItemToView()
    //------------------------------------------------------------------------------------------------------------------------        
    //Adds a new item container to the view, each view can have its way of displaying items
    protected abstract void AddItemToView(Item item); //Differs depending on if it is a list or grid view

    protected private void Update()
    {
        RepopulateItemIconView();

        //Switch between mouse and keyboard controllers
        if (Input.GetKeyUp(KeyCode.K))
        {
            if (shopController is MouseController)
            {
                SwitchToKeyboardControl();
            }
        }

        else if (Input.GetMouseButtonUp(0))
        {
            if (shopController is GridViewKeyboardController || shopController is ListViewKeyboardController)
            {
                SwitchToMouseControl();
            }
        }

        //Let the current controller handle input
        shopController.HandleInput();
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  SwitchToKeyboardControl()
    //------------------------------------------------------------------------------------------------------------------------    
    protected virtual void SwitchToKeyboardControl() //Differs depending on if it is a list or grid view, which will add their own controllers
    {
        Destroy(shopController);//Remove the current controller component
        //shopController = gameObject.AddComponent<ListViewKeyboardController>().Initialize(shopModel);//Create and add a keyboard controller
        instructionText.text = "The current control mode is: Keyboard Control, WASD to select item, press K to buy. Press left mouse button to switch to Mouse Control.";
        actionButton.gameObject.SetActive(false);//Hide the buy button because we only use keyboard

        Debug.Log("Users Controls changed from Mouse to Keyboard!");
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  SwitchToMouseControl()
    //------------------------------------------------------------------------------------------------------------------------ 
    protected private void SwitchToMouseControl()
    {
        Destroy(shopController);//Remove the current controller component
        shopController = gameObject.AddComponent<MouseController>().Initialize(shopModel);//Create and add a mouse controller
        instructionText.text = "The current control mode is: Mouse Control, press 'K' to switch to Keyboard Control.";
        actionButton.gameObject.SetActive(true);//Show the buy button for the mouse controler

        Debug.Log("Users Controls changed from Mouse to Keyboard!");
    }
}
