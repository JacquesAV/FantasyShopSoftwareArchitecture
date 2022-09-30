using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Common factory that allows for creating items at the uncoomon rarity
public class UncommonFactory : ItemFactory
{
    public Weapon CreateWeapon()
    {
        return Weapon.CreateItem(Rarity.Uncommon);
    }
    public Armor CreateArmor()
    {
        return Armor.CreateItem(Rarity.Uncommon);
    }
    public Potion CreatePotion()
    {
        return Potion.CreateItem(Rarity.Uncommon);
    }
}
