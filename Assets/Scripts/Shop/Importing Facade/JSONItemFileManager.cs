using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

//Primary source used for understanding and using JSON by itself:
//https://graymattertutorials.wordpress.com/2018/01/15/serialization-with-json-net-in-unity-3d/
public class JSONItemFileManager : MonoBehaviour
{
    //List that holds all basic item data for weapons, armor and potions
    private static List<BasicItemData> jsonWeapons = new List<BasicItemData>();
    private static List<BasicItemData> jsonArmors = new List<BasicItemData>();
    private static List<BasicItemData> jsonPotions = new List<BasicItemData>();

    private static string primaryFilePath;

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Static Constructor()
    //------------------------------------------------------------------------------------------------------------------------
    //Runs an important process of loading the relevant files
    //Therefore this script should always be constructed when being used
    //A static constructor performs specific actions once only.
    //It is automatically called before any instances are created or referenced
    static JSONItemFileManager()
    {
        //Loads items and checks that the lists were not null/empty
        CouldLoadItemLists();

        //Validate that the item lists are not null
        CouldValidateListItems();
    }

    public static bool CouldValidateListItems()
    {
        bool allValid = true;

        //If they return empty or null, they will reset the lists to avoid errors
        if (IsListNull(ref jsonWeapons)) { allValid = false; };
        if (IsListNull(ref jsonArmors)) { allValid = false; };
        if (IsListNull(ref jsonPotions)) { allValid = false; };

        Debug.Log("JSON Item lists files were validated!");

        //Return that all lists were validated correctly
        return allValid;
    }

    //Checks if the lists are empty and if they are, recreate the list to avoid errors
    private static bool IsListNull(ref List<BasicItemData> itemsListReference)
    {
        if (itemsListReference is null || itemsListReference == null)
        {
            //Reset the list to not be null, but empty
            itemsListReference = new List<BasicItemData>();
            Debug.Log(itemsListReference + "failed to load, was null and emptied");
            return true;
        }
        else
        {
            return false;
        }
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  GetRandomItemJSON()
    //------------------------------------------------------------------------------------------------------------------------
    //These functions return the BasicItemData of a random item from the premade list of potential items
    public static BasicItemData GetRandomWeaponJSON()
    {
        return jsonWeapons[Random.Range(0, jsonWeapons.Count-1)];
    }
    public static BasicItemData GetRandomArmorJSON()
    {
        return jsonArmors[Random.Range(0, jsonArmors.Count-1)];
    }
    public static BasicItemData GetRandomPotionJSON()
    {
        return jsonPotions[Random.Range(0, jsonPotions.Count-1)];
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  LoadItemLists()
    //------------------------------------------------------------------------------------------------------------------------
    //These import the items from the files to their respective list
    //Allows for premade items to be randomly selected
    //They are bools in order to tell if the file was sucessfully loaded
    public static bool CouldLoadItemLists()
    {
        bool allValid = true;
        //Call all loading methods for items
        //If they return false, then we know that some item lists were not loaded correctly
        if (!LoadWeaponList()) { allValid = false; };
        if (!LoadArmorList()) { allValid = false; };
        if (!LoadPotionList()) { allValid = false; };

        Debug.Log("JSON Stored Item lists were loaded!");

        //Return that all lists were loaded correctly
        return allValid;
    }
    public static bool LoadWeaponList()
    {
        //Load list with the correct target list
        return LoadFileToList(ref jsonWeapons, nameof(jsonWeapons));
    }
    public static bool LoadArmorList()
    {
        //Load list with the correct target list
        return LoadFileToList(ref jsonArmors, nameof(jsonArmors));
    }
    public static bool LoadPotionList()
    {
        //Load list with the correct target list
        return LoadFileToList(ref jsonPotions, nameof(jsonPotions));
    }
    
    //Uses a reference so the specific list gets updated correctly
    private static bool LoadFileToList(ref List<BasicItemData> itemsListReference, string targetFile) 
    {
        //Sets the correct file path based on the given 
        primaryFilePath = Path.Combine(Application.streamingAssetsPath, "ItemResourcesJSON", targetFile + ".json");

        //If file does not exist and nothing can be loaded, return as false
        if (!File.Exists(primaryFilePath))
        {
            return false;
        }

        //Open file stream and allow for reading
        using (FileStream stream = new FileStream(primaryFilePath, FileMode.Open, FileAccess.Read))
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                //Read the file and then set the relevant array to 
                string jsonString = reader.ReadToEnd();

                //Set the referenced list to the information contained in the file
                itemsListReference = JsonConvert.DeserializeObject<List<BasicItemData>>(jsonString);
            }
        }
        //Return that list was succesfully loaded
        return true;
    }

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  SaveAllLists()
    //------------------------------------------------------------------------------------------------------------------------
    //This function is primarilly used for testing and is not meant to be used in the final product
    private static void SaveListToFile(ref List<BasicItemData> itemsListReference, string targetFile)
    {
        if (itemsListReference != null)
        {
            //Sets the correct file path based on the given 
            primaryFilePath = Path.Combine(Application.streamingAssetsPath, "ItemResourcesJSON", targetFile + ".json");
            using (FileStream stream = new FileStream(primaryFilePath, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    //Serializes/Prepares the given list to serialize into the files
                    string jsonString = JsonConvert.SerializeObject(itemsListReference);
                    //Writes to the file
                    writer.Write(jsonString);
                }
            }
        }
    }

    //Inputs and commands purely for testing purposes
    //Are not used or needed for the final piece, but are kept
    //for keeping track of progress and quick tests in future
    //void Update()
    //{
        //if(Input.GetKeyUp(KeyCode.I))
        //{
        //    Debug.Log("POPULATING LISTS");
        //    RandomAssistantGenerator();
        //}
        //else if (Input.GetKeyUp(KeyCode.O))
        //{
        //    Debug.Log("SAVING");
        //    SaveListToFile(ref jsonWeapons, nameof(jsonWeapons));
        //    SaveListToFile(ref jsonArmors, nameof(jsonArmors));
        //    SaveListToFile(ref jsonPotions, nameof(jsonPotions));
        //}
        //else if(Input.GetKeyUp(KeyCode.P))
        //{
        //    Debug.Log("LOADING");
        //    LoadItemLists();
        //}
        //else if (Input.GetKeyUp(KeyCode.L))
        //{
        //    Debug.Log("CLEARING");
        //    jsonWeapons.Clear();
        //    jsonArmors.Clear();
        //    jsonPotions.Clear();
        //}
    //}

    //Testing tool which allows for the list of items to be randomly filled
    //Should NOT be used in the final version and is exclusively kept for testing purposes
    //public static void RandomAssistantGenerator()
    //{
    //    for(int i=0; i <= 14; i++)
    //    {
    //        jsonWeapons.Add(new BasicItemData("WeaponName", "items_50", 5, "Attack Damage", 5, "Painful Weapon"));
    //    }
    //    for (int i = 0; i <= 12; i++)
    //    {
    //        jsonArmors.Add(new BasicItemData("ArmorName", "items_51", 5, "Armor Class", 5, "Stronk Armor"));
    //    }
    //    for (int i = 0; i <= 4; i++)
    //    {
    //        jsonPotions.Add(new BasicItemData("PotionName", "items_52", 5, "CUSTOM", 5, "Tasty Juice"));
    //    }
    //}
}
