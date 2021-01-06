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

            //The shop scene only contains one grid buy view, we use Resources.FindObjectsOfTypeAll to get the reference to it,
            //Resources.FFindObjectsOfTypeAll is used instead of GameObject.Find because the later can't find disabled objects
            gridBuyView = Resources.FindObjectsOfTypeAll<ShopGridBuyView>()[0];

            //Active the gridBuyView game object to initialize the class, if we don't do this 'void Start()' won't be called
            //You should active all the game objects that are involved in the test before testing the functions from their components
            gridBuyView.gameObject.SetActive(true);
        }

        // Use meaningful name for your test cases, this case tests if the ShopGridBuyView component has initialized its ShopModel property 
        [UnityTest]
        public IEnumerator ShopGridBuyViewInitializedShopModel()
        {
            yield return null; //yield return null skips one frame, waits for the Unity scene to load

            //now test if a ShopModel is assigned to gridBuyView
            Assert.IsNotNull(gridBuyView.ShopModel, "No BuyModel is assigned in ShopGridBuyView");
        }

        //This case tests if the grid buy view displays the correct amount of Items
        [UnityTest]
        public IEnumerator ShopGridBuyViewDisplaysCorrectAmountOfItems()
        {
            yield return null; //yield return null skips one frame, waits for the Unity scene to load

            //Now that the scene is loaded and the gridBuyView game object was activated in SetupTests(), we can use GameObject.Find
            //to find the game object we want to test
            GameObject gridItemsPanel = GameObject.Find("GridItemsPanel");

            yield return new WaitForEndOfFrame();//Since we are testing how many items are displayed, we should use WaitForEndOfFrame to wait until the end of the frame,
                                                 //so that the view finished updating and rendering everything 

            int itemCount = gridItemsPanel.transform.childCount;
            Assert.AreEqual(gridBuyView.ShopModel.inventory.GetItemCount(), itemCount, "The generated item count is not equal to shopModel's itemCount");
        }

        //This case tests if the buyModel can throw an ArgumentOutOfRangeException when it's asked to select an item by a negative
        //index. Incorrect indexes can be generated from bugs in views or controllers, throwing the correct type of exceptions is
        //better than failing silently for debugging. Your unit tests should cover exception handlings
        [UnityTest]
        public IEnumerator ShopModelThrowsExceptionsWhenSelectingNegativeIndex()
        {
            //yield return null skips one frame, waits for the Unity scene to load and buyModel to be assigned
            yield return null;

            //Creates a delegate that call gridBuyView.ShopModel.SelectItemByIndex(-1), the test runner will run the function, and
            //check if an ArgumentOutOfRangeException is thrown, the unit test would fail if no ArgumentOutOfRangeException
            //was thrown
            Assert.Throws<System.ArgumentOutOfRangeException>(delegate
            {
                gridBuyView.ShopModel.SelectItemByIndex(-1);
            });
        }
    }
}
