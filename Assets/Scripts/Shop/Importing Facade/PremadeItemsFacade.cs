using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the master facade for all premade items imported by the JSON Item File Manager
//It implements the interfaces and providers in a centralized point that can be used by other classes
//This is bare bones and is meant to show the intended structure
//This class and all interfaces and providers can be expanded to include more options than only "random",
//possibly extending into even more specific types of items
public class PremadeItemsFacade
{
    private readonly IWeapon weaponProvider;
    private readonly IArmor armorProvider;
    private readonly IPotion potionProvider;
    public PremadeItemsFacade()
    {
        //Sets all the interfaces to the correct providers
        weaponProvider = new PremadeWeaponProvider();
        armorProvider = new PremadeArmorProvider();
        potionProvider = new PremadePotionProvider();
    }
    public BasicItemData GetWeaponData()
    {
        return weaponProvider.GetRandomWeaponData();
    }
    public BasicItemData GetArmorData()
    {
        return armorProvider.GetRandomArmorData();
    }
    public BasicItemData GetPotionData()
    {
        return potionProvider.GetRandomPotionData();
    }
}
