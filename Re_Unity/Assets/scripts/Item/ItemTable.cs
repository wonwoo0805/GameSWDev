using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemTable", menuName = "Scriptable Objects/ItemTable")]
public class ItemTable : ScriptableObject
{
    public List<ItemData> itemList = new List<ItemData>();

    public ItemData GetRandomItem()
    {
        if(itemList.Count == 0) return null;
        int randomIndex = Random.Range(0,itemList.Count);
        return itemList[randomIndex];
    }
}
