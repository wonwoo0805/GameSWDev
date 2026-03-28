using UnityEngine;

public enum ItemType { Weapon, Armor, Chip, Use }
public enum ItemRarity {  Common, Rare, Epic, Unique, Legendary}

public class ItemData : ScriptableObject
{
    //define string type
    [SerializeField] private string itemName;

    //define int type
    [SerializeField] private int money, weight, num;

    //define image(sprite, etc...) type
    [SerializeField] private Sprite inventoryImage;

    [SerializeField] private ItemType itemType;
    [SerializeField] private ItemRarity itemRarity;

    //access indirectly to inventoryImage
    public Sprite itemDataImage => inventoryImage;
    public ItemType itemDataType => itemType;
    public ItemRarity itemDataRarity => itemRarity;
}
