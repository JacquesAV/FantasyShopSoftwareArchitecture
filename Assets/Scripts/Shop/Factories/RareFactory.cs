using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Common factory that allows for creating items at the rare rarity
public class RareFactory : ItemFactory
{ 
    public Weapon CreateWeapon()
    {
        return Weapon.CreateItem(Rarity.Rare);
    }
    public Armor CreateArmor()
    {
        return Armor.CreateItem(Rarity.Rare);
    }
    public Potion CreatePotion()
    {
        return Potion.CreateItem(Rarity.Rare);
    } 
}
