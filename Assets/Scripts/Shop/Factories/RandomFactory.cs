using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Random factory that allows for creating items with random rarities
public class RandomFactory : ItemFactory
{
    //Helps in deciding how "common" is common
    private readonly float commonRarityRange = 0.6f;
    //Is the rarity fraction of the remaining common rarity range, allows for uncommon and rare items to be calculated
    private readonly float uncommonRarityRange = 0.6f;

    public Weapon CreateWeapon()
    {
        return Weapon.CreateItem(GetRandomRarity());
    }
    public Armor CreateArmor()
    {
        return Armor.CreateItem(GetRandomRarity());
    }
    public Potion CreatePotion()
    {
        return Potion.CreateItem(GetRandomRarity());
    }


    //Gets a random rarity for the item based on the common rarity range
    public Rarity GetRandomRarity()
    {
        //Sets the random range
        float randomRange = Random.Range(0f, 1f);
        //Checks for the range and returns appropriately
        if (randomRange <= commonRarityRange)
        {
            return Rarity.Common;
        }
        else if (randomRange > commonRarityRange && randomRange < (commonRarityRange + (1 - commonRarityRange) * uncommonRarityRange)) //If above common rarity and below rare rarity 
        {
            return Rarity.Uncommon;
        }
        else
        {
            return Rarity.Rare;
        }
    }
}
