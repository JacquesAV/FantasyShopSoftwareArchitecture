using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Derived class of item, it is more specific
//Item should be created using a factory, accessing this class and using the items static facade system
public class Armor : Item
{
    //Constructor that uses the BasicItemData structure
    public Armor(BasicItemData basicData, Rarity pRarity) : base(basicData, pRarity) { }

    public static Armor CreateItem(Rarity pRarity)
    {
        //Returns a potion constructed with data recieved from a JSON file facade system and the given factory rarity
        return new Armor(
            facadeForFactory.GetArmorData(),
            pRarity);
    }
}
