using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemTable", menuName = "Scriptable Objects/ItemTable")]
public class ItemTable : ScriptableObject
{
    public List<ItemData> itemList;
    private Dictionary<ItemRarity, List<ItemData>> itemsByRarity;
    private int itemCount;

    [Header("Rarity Percentage")]
    private int normal = 50;
    private int rare = 30;
    private int epic = 15;
    private int unique = 4;

    public void Initialize(ItemType type1 = ItemType.Any, ItemType type2 = ItemType.Any, 
        ItemType type3 = ItemType.Any, ItemType type4 = ItemType.Any, ItemType type5 = ItemType.Any)
    {
        ItemData[] loadedItems = Resources.LoadAll<ItemData>("ItemData").Where(item => (item.itemDataType != type1) && 
        (item.itemDataType != type2) && (item.itemDataType != type3) && (item.itemDataType != type4) && (item.itemDataType != type5)).ToArray();
        itemList.Clear();

        itemsByRarity = new Dictionary<ItemRarity, List<ItemData>>();

        foreach (ItemData item in loadedItems)
        {
            if (!itemsByRarity.ContainsKey(item.itemDataRarity))
                itemsByRarity[item.itemDataRarity] = new List<ItemData>();
            itemsByRarity[item.itemDataRarity].Add(item);
        }
    }

    public ItemData GetRandomItem()
    {
        // 1단계: 등급 추첨
        ItemRarity rarity = DrawRarity();

        // 2단계: 해당 등급 목록에서 무작위 추첨
        if (!itemsByRarity.ContainsKey(rarity) ||
            itemsByRarity[rarity].Count == 0)
        {
            Debug.Log($"{rarity} 등급 아이템이 없습니다!");
            return null;
        }

        List<ItemData> rarityList = itemsByRarity[rarity];
        return rarityList[Random.Range(0, rarityList.Count)];
    }

    private ItemRarity DrawRarity()
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

        return (selectedRarity);
    }
}
