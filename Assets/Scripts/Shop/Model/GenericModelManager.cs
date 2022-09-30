using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//A generic model manager that should not be used on its own, but could be
//Contains the essentials for any shop manager, but lacks event specific calls
//Can not communicate using events on its own as this should be defined in derived/specialized classes
public class GenericModelManager : MonoBehaviour
{
    [SerializeField]
    protected private int startingItemCount = 16;//, startingMoney = 500;

    //Factory that will be used in generating items, by default it is random
    protected private ItemFactory itemFactory = new RandomFactory();

    //Enum that allows for factory to be chosen from unity editor
    [SerializeField]
    protected private FactoryOptions factoryOption;

    public Inventory modelInventory = new Inventory();

    // Start is called before the first frame update, differs based on the manager
    virtual public void Start()
    {
        SelectFactory();
        InitializeManager(true);
    }
    //------------------------------------------------------------------------------------------------------------------------
    //                                                  InitializeManager()
    //------------------------------------------------------------------------------------------------------------------------
    protected private void InitializeManager(bool isStore) //Accounts for items belonging to a store
    {
        //Initialize the inventory with some starting items
        modelInventory = new Inventory(itemFactory);

        //Populates the inventory using the starting item count, which will use the provided factory
        modelInventory.PopulateInventory(startingItemCount, isStore);
    }
    protected private void InitializeManager(bool isStore, int startingMoney) //Accounts for items belonging to a store and starting money
    {
        //Initialize the inventory with some starting items
        modelInventory = new Inventory(itemFactory, startingMoney);

        //Populates the inventory using the starting item count, which will use the provided factory
        modelInventory.PopulateInventory(startingItemCount, isStore);
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  SelectFactory()
    //------------------------------------------------------------------------------------------------------------------------
    //Switch branches that allow for the factory to be chosen based on the inspector input
    protected private void SelectFactory()
    {
        switch (factoryOption)
        {
            case FactoryOptions.CommonFactory:
                itemFactory = new CommonFactory();
                break;

            case FactoryOptions.UncommonFactory:
                itemFactory = new UncommonFactory();
                break;

            case FactoryOptions.RareFactory:
                itemFactory = new RareFactory();
                break;

            case FactoryOptions.RandomFactory:
                itemFactory = new RandomFactory();
                break;

            default:
                itemFactory = new RandomFactory();
                break;
        }
    }
}
