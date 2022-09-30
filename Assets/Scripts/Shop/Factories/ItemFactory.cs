using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interface that allows for easier interaction with the different types of factories
//The methods in this interface will call the relevant methods in the classes using the interface
//This can then be further used through more specific factory types
public interface ItemFactory
{
    Weapon CreateWeapon();
    Armor CreateArmor();
    Potion CreatePotion();
}
