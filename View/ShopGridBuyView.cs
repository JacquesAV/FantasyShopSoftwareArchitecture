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
public class ShopGridBuyView : MonoBehaviour
{ 
    public ShopModel ShopModel => shopModel; //A getter to access shopModel.

    [SerializeField]
    private GridLayoutGroup itemLayoutGroup; //Links to a GridLayoutGroup in the Unity scene

    [SerializeField]
    private GameObject itemPrefab; //A prefab to display an item in the view

    [SerializeField]
    private Button buyButton; 

    [SerializeField]
    private TextMeshProUGUI instructionText;

    private ViewConfig viewConfig; //To set up the grid view, we need to know how many columns the grid view has, in the current setup,
                                   //this information can be found in a ViewConfig scriptable object, which serves as a configuration file for
                                   //views.

    private ShopModel shopModel; //Model in MVC pattern
    private ShopController shopController; //Controller in MVC pattern

    private void Start()
    {
        shopModel = new BuyModel(2f, 16, 500); //Right now use magic values to set up the shop
        shopController = gameObject.AddComponent<MouseController>().Initialize(shopModel);//Set the default controller to be the mouse controller
        viewConfig = Resources.Load<ViewConfig>("ViewConfig");//Load the ViewConfig scriptable object from the Resources folder
        Debug.Assert(viewConfig != null);
        SetupItemIconView(); //Setup the grid view's properties
        PopulateItemIconView(); //Display items
        InitializeButtons(); //Connect the buttons to the controller
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  SetupItemIconView()
    //------------------------------------------------------------------------------------------------------------------------        
    //Setup the grid view according to the ViewConfig object's requirements, right now it just sets the constraint mode and column count,
    //you can make cosmetic adjustments to the GridLayoutGroup by adding more configurations to ViewConfig and use them adjusting properties
    //like cellSize, spacing, padding, etc.
    private void SetupItemIconView()
    {
        itemLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;//Set the constraint mode of the GridLayoutGroup
        itemLayoutGroup.constraintCount = viewConfig.gridViewColumnCount; //Set the column count according to the ViewConfig object
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  RepopulateItems()
    //------------------------------------------------------------------------------------------------------------------------        
    //Clears the grid view and repopulates it with new icons (updates the visible icons)
    private void RepopulateItemIconView() {
        ClearIconView();
        PopulateItemIconView();
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  PopulateItems()
    //------------------------------------------------------------------------------------------------------------------------        
    //Adds one icon for each item in the shop
    private void PopulateItemIconView() {
        foreach (Item item in shopModel.inventory.GetItems()) {
            AddItemToView(item);
        }
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  ClearIconView()
    //------------------------------------------------------------------------------------------------------------------------        
    //Removes all existing icons in the gridview
    private void ClearIconView() {
        Transform[] allIcons = itemLayoutGroup.transform.GetComponentsInChildren<Transform>();
        foreach (Transform child in allIcons) {
                if (child != itemLayoutGroup.transform) {
                    Destroy(child.gameObject);
                }
            }
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  AddItemToView()
    //------------------------------------------------------------------------------------------------------------------------        
    //Adds a new item container to the view, each view can have its way of displaying items
    private void AddItemToView(Item item) {
        GameObject newItemIcon = GameObject.Instantiate(itemPrefab);
        newItemIcon.transform.SetParent(itemLayoutGroup.transform);
        newItemIcon.transform.localScale = Vector3.one;//The scale would automatically change in Unity so we set it back to Vector3.one.

        GridViewItemContainer itemContainer = newItemIcon.GetComponent<GridViewItemContainer>();
        Debug.Assert(itemContainer != null);
        bool isSelected = (item == shopModel.GetSelectedItem());
        itemContainer.Initialize(item, isSelected);
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  InitializeButtons()
    //------------------------------------------------------------------------------------------------------------------------        
    //This method adds a listener to the 'Buy' button. They are forwarded to the controller. Since this is the confirm button of
    //the buy view, it will just call the controller interface's ConfirmSelectedItem function, the controller will handle the rest.
    private void InitializeButtons() {
        buyButton.onClick.AddListener(
            delegate {
                shopController.ConfirmSelectedItem();
            }
        );
    }

    private void Update()
    {
        RepopulateItemIconView();//Repopulate the view each frame, this is very inefficient and won't work in many scenarios and SHOULD NOT be in
                                 //the final implementation, the view should be modified by the models via an observer or event queue pattern

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
            if (shopController is GridViewKeyboardController)
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
    private void SwitchToKeyboardControl()
    {
        Destroy(shopController);//Remove the current controller component
        shopController = gameObject.AddComponent<GridViewKeyboardController>().Initialize(shopModel);//Create and add a keyboard controller
        instructionText.text = "The current control mode is: Keyboard Control, WASD to select item, press K to buy. Press left mouse button to switch to Mouse Control.";
        buyButton.gameObject.SetActive(false);//Hide the buy button because we only use keyboard
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  SwitchToMouseControl()
    //------------------------------------------------------------------------------------------------------------------------ 
    private void SwitchToMouseControl()
    {
        Destroy(shopController);//Remove the current controller component
        shopController = gameObject.AddComponent<MouseController>().Initialize(shopModel);//Create and add a mouse controller
        instructionText.text = "The current control mode is: Mouse Control, press 'K' to switch to Keyboard Control.";
        buyButton.gameObject.SetActive(true);//Show the buy button for the mouse controler
    }
}
