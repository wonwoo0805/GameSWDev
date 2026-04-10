using UnityEngine;

public enum ItemType { Weapon, Armor, Chip, Any, Use }
public enum ItemRarity {  Common, Rare, Epic, Unique, Legendary}

[CreateAssetMenu(fileName = "New Item", menuName = "Items/ItemData")]
public class ItemData : ScriptableObject
{
    //define string type
    [SerializeField] private string itemName;

    //define int type
    [SerializeField] private int money, weight, num;

    //define image(sprite, etc...) type
    [SerializeField] private Sprite inventoryImage;
    [SerializeField] private GameObject prefab;
    [SerializeField] private ItemType itemType;
    [SerializeField] private ItemRarity itemRarity;


    //access indirectly to inventoryImage
    public Sprite itemDataImage => inventoryImage;
    public string name => itemName;
    public ItemType itemDataType => itemType;
    public GameObject itemPrefab => prefab;
    public ItemRarity itemDataRarity => itemRarity;
    public int itemDataNum => num;

}
