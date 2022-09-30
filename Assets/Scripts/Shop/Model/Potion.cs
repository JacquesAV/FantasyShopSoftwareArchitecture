using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Derived class of item, it is more specific
//Item should be created using a factory, accessing this class and using the items static facade system
public class Potion : Item
{
    //Constructor that uses the BasicItemData structure
    public Potion(BasicItemData basicData, Rarity pRarity) : base(basicData, pRarity) { }

    public static Potion CreateItem(Rarity pRarity)
    {
        //Returns a potion constructed with data recieved from a JSON file facade system and the given factory rarity
        return new Potion(
            facadeForFactory.GetPotionData(),
            pRarity);
    }
}
