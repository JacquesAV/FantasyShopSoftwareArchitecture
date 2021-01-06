/// <summary>
/// This class holds data for an Item. Currently it has a name, an iconName and a base price.
/// </summary>
public class Item
{
    public readonly string name;
    public readonly string iconName;
    public int basePrice { get; private set; } // This is the base price for the item, the buying and selling prices can be
                                               // generated based on this value. 

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Item()
    //------------------------------------------------------------------------------------------------------------------------
    public Item(string name, string iconName, int pbasePrice)
    {
        this.name = name;
        this.iconName = iconName;
        this.basePrice = pbasePrice;
    }

}

