//------------------------------------------------------------------------------------------------------------------------
//                                                  BasicItemData
//------------------------------------------------------------------------------------------------------------------------
//Important data struct that contains all information that is essential when creating any item, regardless of type
//After this data is loaded or implemented, then other derived information is caluclated
//Example: basePrice is declared here and when "Weapon" is constructed a separate function is called to calculate selling and buying prices
public struct BasicItemData
{
    //Item stats that define information about the item
    public string itemName; //Item name
    public string iconName; //Icon that will be used
    public int basePrice; //Base price of the item, modified for buying/selling
    public string propertyName; //Name of the property
    public int basePropertyValue; // This is the base property for the item
    public string itemDescription; //Description of the item

    public BasicItemData(string givenName, string givenIconName, int givenBasePrice, string givenPropertyName, int givenPropertyValue, string givenDescription)
    {
        this.itemName = givenName;
        this.iconName = givenIconName;
        this.basePrice = givenBasePrice;
        this.propertyName = givenPropertyName;
        this.basePropertyValue = givenPropertyValue;
        this.itemDescription = givenDescription;
    }
}
