using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Common factory that allows for creating items at the common rarity
public class CommonFactory : ItemFactory
{
    public Weapon CreateWeapon()
    {
        return Weapon.CreateItem(Rarity.Common);
    }
    public Armor CreateArmor()
    {
        return Armor.CreateItem(Rarity.Common);
    }
    public Potion CreatePotion()
    {
        return Potion.CreateItem(Rarity.Common);
    }
}
