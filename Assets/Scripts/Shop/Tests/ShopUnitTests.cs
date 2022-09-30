using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class ShopUnitTests
    {
        private ShopGridBuyView gridBuyView;//This is the grid buy view we want to test
        private ShopListBuyView listBuyView;//This is the list buy view we want to test
        private ShopGridSellView gridSellView;//This is the grid sell view we want to test
        private ShopListSellView listSellView;//This is the list sell view we want to test
        private ShopGridUpgradeView gridUpgradeView;//This is the grid upgrade view we want to test
        private ShopListUpgradeView listUpgradeView;//This is the list upgrade view we want to test

        private PlayerModelManager playerModelManager;//This is the player model manager to test
        private ShopModelManager shopModelManager;//This is the shop model manager to test
        private EventManager eventManager;//This is the event manager to test
        private ViewConfig viewConfig;//This is the view config to test

        //Setup the test scene
        [OneTimeSetUp]
        public void LoadShopScene()
        {
            // Load the Scene to do unit test. In the scope of this project, this is fine. In a more complicated project, a game scene could take
            // a long time to load, in which case it's better to create test scenes to do unit tests
            SceneManager.LoadScene(0);
        }

        //Setup the unit tests here
        [UnitySetUp]
        public IEnumerator SetupTests()
        {
            yield return null; //yield return null skips one frame, this is to make sure that this happens after the scene is loaded

            //Resources.FindObjectsOfTypeAll is used instead of GameObject.Find because the later can't find disabled objects
            gridBuyView = Resources.FindObjectsOfTypeAll<ShopGridBuyView>()[0];
            listBuyView = Resources.FindObjectsOfTypeAll<ShopListBuyView>()[0];
            gridSellView = Resources.FindObjectsOfTypeAll<ShopGridSellView>()[0];
            listSellView = Resources.FindObjectsOfTypeAll<ShopListSellView>()[0];
            gridUpgradeView = Resources.FindObjectsOfTypeAll<ShopGridUpgradeView>()[0];
            listUpgradeView = Resources.FindObjectsOfTypeAll<ShopListUpgradeView>()[0];

            //Manager/Config related find statements
            playerModelManager = Resources.FindObjectsOfTypeAll<PlayerModelManager>()[0];
            shopModelManager = Resources.FindObjectsOfTypeAll<ShopModelManager>()[0];
            eventManager = Resources.FindObjectsOfTypeAll<EventManager>()[0]; 
            viewConfig = Resources.Load<ViewConfig>("ViewConfig");

            //Active the game objects to initialize the class, if we don't do this 'void Start()' won't be called
            //You should active all the game objects that are involved in the test before testing the functions from their components
            gridBuyView.gameObject.SetActive(true);
            listBuyView.gameObject.SetActive(true);
            gridSellView.gameObject.SetActive(true);
            listSellView.gameObject.SetActive(true);
            gridUpgradeView.gameObject.SetActive(true);
            listUpgradeView.gameObject.SetActive(true);

            playerModelManager.gameObject.SetActive(true);
            shopModelManager.gameObject.SetActive(true);
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  ShopGridBuy Related
        //------------------------------------------------------------------------------------------------------------------------   
        //This case tests if the ShopGridBuyView component has initialized its ShopModel property 
        [UnityTest]
        public IEnumerator ShopGridBuyViewInitializedShopModel()
        {
            yield return null; //yield return null skips one frame, waits for the Unity scene to load

            //now test if a ShopModel is assigned to gridBuyView
            Assert.IsNotNull(gridBuyView.ShopModel, "No BuyModel is assigned in ShopGridBuyView");
        }

        //This case tests if the ShopGridBuyView has a non null inventory
        [UnityTest]
        public IEnumerator ShopGridBuyViewHasInventory()
        {
            yield return null; //yield return null skips one frame, waits for the Unity scene to load

            //Now test if a ShopModel has an inventory
            Assert.IsNotNull(gridBuyView.ShopModel.shopInventory, "No Inventory is assigned in ShopGridBuyView");
        }

        //This case tests if the buy grid model is using the correct shop inventory
        [UnityTest]
        public IEnumerator ShopGridBuyViewHasCorrectInventory()
        {
            //yield return null skips one frame, waits for the Unity scene to load and buyModel to be assigned
            yield return null;

            //Ensures that the correct inventory references are being used
            Assert.AreSame(gridBuyView.ShopModel.shopInventory, shopModelManager.modelInventory, "ShopGridBuyView is not sharing the correct shop inventory!");
        }

        //This case tests if the ShopGridBuyView displays the correct amount of Items
        [UnityTest]
        public IEnumerator ShopGridBuyViewDisplaysCorrectAmountOfItems()
        {
            yield return null; //yield return null skips one frame, waits for the Unity scene to load

            //Now that the scene is loaded and the gridBuyView game object was activated in SetupTests(),
            //we can use GameObject.Find to find the game object we want to test
            GameObject itemsPanel = GameObject.Find("GridBuyItemsPanel");

            yield return new WaitForEndOfFrame();//Since we are testing how many items are displayed, we should use WaitForEndOfFrame to wait until the end of the frame,
                                                 //so that the view finished updating and rendering everything 

            int itemCount = itemsPanel.transform.childCount;
            Assert.AreEqual(gridBuyView.ShopModel.shopInventory.GetItemCount(), itemCount, "The generated item count is not equal to shopModel's itemCount");
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  ShopListBuy Related
        //------------------------------------------------------------------------------------------------------------------------   
        //This case tests if the ShopListBuyView component has initialized its ShopModel property 
        [UnityTest]
        public IEnumerator ShopListBuyViewInitializedShopModel()
        {
            yield return null; //yield return null skips one frame, waits for the Unity scene to load

            //now test if a ShopModel is assigned to gridBuyView
            Assert.IsNotNull(listBuyView.ShopModel, "No BuyModel is assigned in ShopListBuyView");
        }

        //This case tests if the shopview has a non null inventory
        [UnityTest]
        public IEnumerator ShopListBuyViewHasInventory()
        {
            yield return null; //yield return null skips one frame, waits for the Unity scene to load

            //Now test if a ShopModel has an inventory
            Assert.IsNotNull(listBuyView.ShopModel.shopInventory, "No Inventory is assigned in ShopListBuyView");
        }

        //This case tests if the buy list model is using the correct shop inventory
        [UnityTest]
        public IEnumerator ShopListBuyViewHasCorrectInventory()
        {
            //yield return null skips one frame, waits for the Unity scene to load and buyModel to be assigned
            yield return null;

            //Ensures that the correct inventory references are being used
            Assert.AreSame(listBuyView.ShopModel.shopInventory, shopModelManager.modelInventory, "ShopListBuyView is not sharing the correct shop inventory!");
        }

        //This case tests if the ShopListBuyView displays the correct amount of Items
        [UnityTest]
        public IEnumerator ShopListBuyViewDisplaysCorrectAmountOfItems()
        {
            yield return null; //yield return null skips one frame, waits for the Unity scene to load

            //Now that the scene is loaded and the listBuyView game object was activated in SetupTests(),
            //we can use GameObject.Find to find the game object we want to test
            GameObject itemsPanel = GameObject.Find("ListBuyItemContentHolder");

            yield return new WaitForEndOfFrame();//Since we are testing how many items are displayed, we should use WaitForEndOfFrame to wait until the end of the frame,
                                                 //so that the view finished updating and rendering everything 

            int itemCount = itemsPanel.transform.childCount;
            Assert.AreEqual(listBuyView.ShopModel.shopInventory.GetItemCount(), itemCount, "The generated item count is not equal to shopModel's itemCount");
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  ShopGridSell Related
        //------------------------------------------------------------------------------------------------------------------------   
        //This case tests if the ShopGridSellView component has initialized its ShopModel property 
        [UnityTest]
        public IEnumerator ShopGridSellViewInitializedShopModel()
        {
            yield return null; //yield return null skips one frame, waits for the Unity scene to load

            //now test if a ShopModel is assigned to gridBuyView
            Assert.IsNotNull(gridSellView.ShopModel, "No SellModel is assigned in ShopGridSellView");
        }

        //This case tests if the shopview has a non null inventory
        [UnityTest]
        public IEnumerator ShopGridSellViewHasInventory()
        {
            yield return null; //yield return null skips one frame, waits for the Unity scene to load

            //Now test if a ShopModel has an inventory
            Assert.IsNotNull(gridSellView.ShopModel.shopInventory, "No Inventory is assigned in ShopGridSellView");
        }

        //This case tests if the sell grid model is using the correct shop inventory
        [UnityTest]
        public IEnumerator ShopGridSellViewHasCorrectInventory()
        {
            //yield return null skips one frame, waits for the Unity scene to load and sellModel to be assigned
            yield return null;

            //Ensures that the correct inventory references are being used
            Assert.AreSame(gridSellView.ShopModel.shopInventory, playerModelManager.modelInventory, "ShopGridSellView is not sharing the correct shop inventory!");
        }

        //This case tests if the ShopGridSellView displays the correct amount of Items
        [UnityTest]
        public IEnumerator ShopGridSellViewDisplaysCorrectAmountOfItems()
        {
            yield return null; //yield return null skips one frame, waits for the Unity scene to load

            //Now that the scene is loaded and the gridSellView game object was activated in SetupTests(),
            //we can use GameObject.Find to find the game object we want to test
            GameObject itemsPanel = GameObject.Find("GridSellItemsPanel");

            yield return new WaitForEndOfFrame();//Since we are testing how many items are displayed, we should use WaitForEndOfFrame to wait until the end of the frame,
                                                 //so that the view finished updating and rendering everything 

            int itemCount = itemsPanel.transform.childCount;
            Assert.AreEqual(gridSellView.ShopModel.shopInventory.GetItemCount(), itemCount, "The generated item count is not equal to shopModel's itemCount");
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  ShopListSell Related
        //------------------------------------------------------------------------------------------------------------------------   
        //This case tests if the ShopListSellView component has initialized its ShopModel property 
        [UnityTest]
        public IEnumerator ShopListSellViewInitializedShopModel()
        {
            yield return null; //yield return null skips one frame, waits for the Unity scene to load

            //now test if a ShopModel is assigned to gridBuyView
            Assert.IsNotNull(listSellView.ShopModel, "No SellModel is assigned in ShopListSellView");
        }

        //This case tests if the shopview has a non null inventory
        [UnityTest]
        public IEnumerator ShopListSellViewHasInventory()
        {
            yield return null; //yield return null skips one frame, waits for the Unity scene to load

            //Now test if a ShopModel has an inventory
            Assert.IsNotNull(listSellView.ShopModel.shopInventory, "No Inventory is assigned in ShopListSellView");
        }

        //This case tests if the sell list model is using the correct shop inventory
        [UnityTest]
        public IEnumerator ShopListSellViewHasCorrectInventory()
        {
            //yield return null skips one frame, waits for the Unity scene to load and sellModel to be assigned
            yield return null;

            //Ensures that the correct inventory references are being used
            Assert.AreSame(listSellView.ShopModel.shopInventory, playerModelManager.modelInventory, "ShopListSellView is not sharing the correct shop inventory!");
        }

        //This case tests if the ShopListSellView displays the correct amount of Items
        [UnityTest]
        public IEnumerator ShopListSellViewDisplaysCorrectAmountOfItems()
        {
            yield return null; //yield return null skips one frame, waits for the Unity scene to load

            //Now that the scene is loaded and the listSellView game object was activated in SetupTests(),
            //we can use GameObject.Find to find the game object we want to test
            GameObject itemsPanel = GameObject.Find("ListSellItemContentHolder");

            yield return new WaitForEndOfFrame();//Since we are testing how many items are displayed, we should use WaitForEndOfFrame to wait until the end of the frame,
                                                 //so that the view finished updating and rendering everything 

            int itemCount = itemsPanel.transform.childCount;
            Assert.AreEqual(listSellView.ShopModel.shopInventory.GetItemCount(), itemCount, "The generated item count is not equal to shopModel's itemCount");
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  ShopGridUpgrade Related
        //------------------------------------------------------------------------------------------------------------------------   
        //This case tests if the ShopGridUpgradeView component has initialized its ShopModel property 
        [UnityTest]
        public IEnumerator ShopGridUpgradeViewInitializedShopModel()
        {
            yield return null; //yield return null skips one frame, waits for the Unity scene to load

            //now test if a ShopModel is assigned to gridBuyView
            Assert.IsNotNull(gridUpgradeView.ShopModel, "No SellModel is assigned in ShopGridUpgradeView");
        }

        //This case tests if the shopview has a non null inventory
        [UnityTest]
        public IEnumerator ShopGridUpgradeViewHasInventory()
        {
            yield return null; //yield return null skips one frame, waits for the Unity scene to load

            //Now test if a ShopModel has an inventory
            Assert.IsNotNull(gridUpgradeView.ShopModel.shopInventory, "No Inventory is assigned in ShopGridUpgradeView");
        }

        //This case tests if the upgrade grid model is using the correct shop inventory
        [UnityTest]
        public IEnumerator ShopGridUpgradeViewHasCorrectInventory()
        {
            //yield return null skips one frame, waits for the Unity scene to load and upgradeModel to be assigned
            yield return null;

            //Ensures that the correct inventory references are being used
            Assert.AreSame(gridUpgradeView.ShopModel.shopInventory, playerModelManager.modelInventory, "ShopGridUpgradeView is not sharing the correct shop inventory!");
        }

        //This case tests if the ShopGridUpgradeView displays the correct amount of Items
        [UnityTest]
        public IEnumerator ShopGridUpgradeViewDisplaysCorrectAmountOfItems()
        {
            yield return null; //yield return null skips one frame, waits for the Unity scene to load

            //Now that the scene is loaded and the gridUpgradeView game object was activated in SetupTests(),
            //we can use GameObject.Find to find the game object we want to test
            GameObject itemsPanel = GameObject.Find("GridUpgradeItemsPanel");

            yield return new WaitForEndOfFrame();//Since we are testing how many items are displayed, we should use WaitForEndOfFrame to wait until the end of the frame,
                                                 //so that the view finished updating and rendering everything 

            int itemCount = itemsPanel.transform.childCount;
            Assert.AreEqual(gridUpgradeView.ShopModel.shopInventory.GetItemCount(), itemCount, "The generated item count is not equal to shopModel's itemCount");
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  ShopListUpgrade Related
        //------------------------------------------------------------------------------------------------------------------------   
        //This case tests if the ShopListUpgradeView component has initialized its ShopModel property 
        [UnityTest]
        public IEnumerator ShopListUpgradeViewInitializedShopModel()
        {
            yield return null; //yield return null skips one frame, waits for the Unity scene to load

            //now test if a ShopModel is assigned to gridBuyView
            Assert.IsNotNull(listUpgradeView.ShopModel, "No SellModel is assigned in ShopListUpgradeView");
        }

        //This case tests if the shopview has a non null inventory
        [UnityTest]
        public IEnumerator ShopListUpgradeViewHasInventory()
        {
            yield return null; //yield return null skips one frame, waits for the Unity scene to load

            //Now test if a ShopModel has an inventory
            Assert.IsNotNull(listUpgradeView.ShopModel.shopInventory, "No Inventory is assigned in ShopListUpgradeView");
        }

        //This case tests if the upgrade list model is using the correct shop inventory
        [UnityTest]
        public IEnumerator ShopListUpgradeViewHasCorrectInventory()
        {
            //yield return null skips one frame, waits for the Unity scene to load and upgradeModel to be assigned
            yield return null;

            //Ensures that the correct inventory references are being used
            Assert.AreSame(listUpgradeView.ShopModel.shopInventory, playerModelManager.modelInventory, "ShopListUpgradeView is not sharing the correct shop inventory!");
        }

        //This case tests if the ShopListUpgradeView displays the correct amount of Items
        [UnityTest]
        public IEnumerator ShopListUpgradeViewDisplaysCorrectAmountOfItems()
        {
            yield return null; //yield return null skips one frame, waits for the Unity scene to load

            //Now that the scene is loaded and the listUpgradeView game object was activated in SetupTests(),
            //we can use GameObject.Find to find the game object we want to test
            GameObject itemsPanel = GameObject.Find("ListUpgradeItemContentHolder");

            yield return new WaitForEndOfFrame();//Since we are testing how many items are displayed, we should use WaitForEndOfFrame to wait until the end of the frame,
                                                 //so that the view finished updating and rendering everything 

            int itemCount = itemsPanel.transform.childCount;
            Assert.AreEqual(listUpgradeView.ShopModel.shopInventory.GetItemCount(), itemCount, "The generated item count is not equal to shopModel's itemCount");
        }

        //------------------------------------------------------------------------------------------------------------------------
        //                                                  Miscelaneous Related
        //------------------------------------------------------------------------------------------------------------------------  
        //This case tests if any of the shop models can throw an ArgumentOutOfRangeException when it's asked to select an item by a negative
        //index. Incorrect indexes can be generated from bugs in views or controllers, throwing the correct type of exceptions is
        //better than failing silently for debugging. Your unit tests should cover exception handlings
        [UnityTest]
        public IEnumerator ShopModelThrowsExceptionsWhenSelectingNegativeIndex()
        {
            //yield return null skips one frame, waits for the Unity scene to load
            yield return null;

            //Creates a delegate call, the test runner will run the function
            //and check if an ArgumentOutOfRangeException is thrown,
            //the unit test would fail if no ArgumentOutOfRangeException was thrown
            //Will test all model views in case of error
            Assert.Throws<System.ArgumentOutOfRangeException>(delegate { gridBuyView.ShopModel.SelectItemByIndex(-1);});
            Assert.Throws<System.ArgumentOutOfRangeException>(delegate { listBuyView.ShopModel.SelectItemByIndex(-1);});
            Assert.Throws<System.ArgumentOutOfRangeException>(delegate { gridSellView.ShopModel.SelectItemByIndex(-1);});
            Assert.Throws<System.ArgumentOutOfRangeException>(delegate { listSellView.ShopModel.SelectItemByIndex(-1);});
            Assert.Throws<System.ArgumentOutOfRangeException>(delegate { gridUpgradeView.ShopModel.SelectItemByIndex(-1);});
            Assert.Throws<System.ArgumentOutOfRangeException>(delegate { listUpgradeView.ShopModel.SelectItemByIndex(-1);});
        }

        //This case tests if the user inventory exists
        [UnityTest]
        public IEnumerator UserInventoryExists()
        {
            //yield return null skips one frame, waits for the Unity scene to load
            yield return null;

            //Now test if a ShopModel has an inventory
            Assert.IsNotNull(playerModelManager.modelInventory, "No Inventory is assigned in the PlayerModelManager");
        }

        //This case tests if the shop inventory exists
        [UnityTest]
        public IEnumerator ShopInventoryExists()
        {
            //yield return null skips one frame, waits for the Unity scene to load
            yield return null;

            //Now test if a ShopModel has an inventory
            Assert.IsNotNull(shopModelManager.modelInventory, "No Inventory is assigned in the ShopModelManager");
        }

        //This case tests if the buy models are using the same inventory
        [UnityTest]
        public IEnumerator ShopBuyModelsShareSameInventory()
        {
            //yield return null skips one frame, waits for the Unity scene to load
            yield return null;

            //Ensures that the shop views are sharing the correct inventory references
            Assert.AreSame(gridBuyView.ShopModel.shopInventory, listBuyView.ShopModel.shopInventory, "Buy Views are not sharing the same inventories when they should!");
        }

        //This case tests if the sell models are using the same inventory
        [UnityTest]
        public IEnumerator ShopSellModelsShareSameInventories()
        {
            //yield return null skips one frame, waits for the Unity scene to load
            yield return null;

            //Ensures that the shop views are sharing the correct inventory references
            Assert.AreSame(gridSellView.ShopModel.shopInventory, listSellView.ShopModel.shopInventory, "Sell Views are not sharing the same inventories when they should!");
        }

        //This case tests if the upgrade models are using the same inventory
        [UnityTest]
        public IEnumerator ShopUpgradeModelsShareSameInventories()
        {
            //yield return null skips one frame, waits for the Unity scene to load
            yield return null;

            //Ensures that the shop views are sharing the correct inventory references
            Assert.AreSame(gridUpgradeView.ShopModel.shopInventory, listUpgradeView.ShopModel.shopInventory, "Upgrade Views are not sharing the same inventories when they should!");
        }

        //This case tests if the event manager exists
        [UnityTest]
        public IEnumerator EventManagerExists()
        {
            //yield return null skips one frame, waits for the Unity scene to load
            yield return null;

            //Ensures that the shop views are sharing the correct inventory references
            Assert.IsNotNull(eventManager, "Event manager was found to be null!");
        }

        //This case tests if the JSON manager is functioning and loading data
        [UnityTest]
        public IEnumerator JSONItemDataLoaded()
        {
            //yield return null skips one frame, waits for the Unity scene to load
            yield return null;

            //Checks that the JSONItemFileManager can sucessfully load all item files
            Assert.IsTrue(JSONItemFileManager.CouldLoadItemLists(), "JSON Item data was not sucessfully loaded!");
        }
        //This case tests if the JSON manager is functioning
        [UnityTest]
        public IEnumerator JSONItemDataValidated()
        {
            //yield return null skips one frame, waits for the Unity scene to load
            yield return null;

            //Checks that the JSONItemFileManager can sucessfully check that list items have content in them
            Assert.IsTrue(JSONItemFileManager.CouldValidateListItems(), "JSON Item data was not sucessfully validated!");
        }

        //This case tests if the view config is loading correctly
        [UnityTest]
        public IEnumerator ViewConfigLoaded()
        {
            //yield return null skips one frame, waits for the Unity scene to load
            yield return null;

            //Checks that the View config was correctly loaded
            Assert.IsNotNull(viewConfig, "ViewConfig was found to be null even after loading!");
        }
    }
}
