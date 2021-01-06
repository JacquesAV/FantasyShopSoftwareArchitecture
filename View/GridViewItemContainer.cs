using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

/// <summary>
/// This class is applied to a button that represents an Item in the View. It is a visual representation of the item
/// when it is visible in the store. The class holds a link to the original Item, it sets the icon of the button to
/// the one specified in the Item data, and it enables or disables the infoPanel to indicate if the item is selected
/// and display the details of the item.
/// </summary>
public class GridViewItemContainer : MonoBehaviour, IItemContainer
{
    public Item Item => item;//Public getter for the item, required by IItemContainer interface.


    //Link to the highlight image (set in prefab)
    [SerializeField]
    private GameObject highLight;

    //Link to the infomation panel (set in prefab)
    [SerializeField]
    private GameObject infoPanel;

    [SerializeField]
    private Image icon;

    //Link to the atlas of all the item icons, use to retrieve sprites for items. For more information of the API check:
    // https://docs.unity3d.com/2019.3/Documentation/Manual/class-SpriteAtlas.html
    [SerializeField]
    private SpriteAtlas iconAtlas;

    //link to the original item (set in Initialize)
    private Item item;

    //------------------------------------------------------------------------------------------------------------------------
    //                                                  Initialize()
    //------------------------------------------------------------------------------------------------------------------------
    public void Initialize(Item item, bool isSelected) {
        //Stores the item
        this.item = item;

        //Sets the highlight image and infoPanel's visibility
        if (isSelected) {
            highLight.SetActive(true);
            infoPanel.SetActive(true);
        }

        // Clones the first Sprite in the icon atlas that matches the iconName and uses it as the sprite of the icon image.
        Sprite sprite = iconAtlas.GetSprite(item.iconName);

        if (sprite != null) {
            icon.sprite = sprite;
        }
    }
}
