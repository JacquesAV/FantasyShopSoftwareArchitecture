using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Provider that uses the interface in order to get a random item data
//This is bare bones and is meant to show the intended structure
//This class and interfaces can be expanded to include more options than only "random",
//possibly extending into even more specific types or clasifications of these items
public class PremadeWeaponProvider : IWeapon
{
    public BasicItemData GetRandomWeaponData()
    {
        return JSONItemFileManager.GetRandomWeaponJSON();
    }
}