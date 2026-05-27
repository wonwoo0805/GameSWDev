using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemTable", menuName = "Scriptable Objects/ItemTable")]
public class ItemTable : MonoBehaviour
{
    public List<ItemData> itemList = new List<ItemData>();
    private int itemCount;

    [Header("Rarity Percentage")]
    private int normal = 50;
    private int rare = 30;
    private int epic = 15;
    private int unique = 4;
    private int legendary = 1;

    private void Start()
    {
        ItemData[] loadedItems = Resources.LoadAll<ItemData>("ItemData");
        itemList.Clear();
        itemList.AddRange(loadedItems);
        itemCount = loadedItems.Length;
    }

    public ItemData GetRandomItem()
    {
        
        //if (itemCount == 0) return null;
        //int randomIndex = Random.Range(0, itemCount);
        //return itemList[randomIndex];

        var (rarity, code) = DrawRarityAndCode();

        //find item with code and rarity
        ItemData foundItem = itemList.Find(
            item => item.itemDataRarity == rarity &&
                    item.itemDataCode == code
        );

        return foundItem;
    }

    private (ItemRarity rarity, int code) DrawRarityAndCode()
    {
        //draw itemRarity
        int rarityRoll = Random.Range(1, 101);
        ItemRarity selectedRarity;

        if (rarityRoll <= normal)
            selectedRarity = ItemRarity.Normal;
        else if (rarityRoll <= normal + rare)
            selectedRarity = ItemRarity.Rare;
        else if (rarityRoll <= normal + rare + epic)
            selectedRarity = ItemRarity.Epic;
        else if (rarityRoll <= normal + rare + epic + unique)
            selectedRarity = ItemRarity.Unique;
        else
            selectedRarity = ItemRarity.Legendary;

        //draw itemCode
        int code = Random.Range(0, itemCount);

        return (selectedRarity, code);
    }
}
