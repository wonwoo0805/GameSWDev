using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemSlot> inventory = new List<ItemSlot>();

    // 지정된 개수만큼 데이터 빈 칸 만들기
    public void InitializeData(int size)
    {
        inventory.Clear();
        for (int i = 0; i < size; i++)
        {
            inventory.Add(ScriptableObject.CreateInstance<ItemSlot>());
        }
    }

    // 아이템 추가 로직 (기존 코드 그대로)
    public int addItem(ItemData newItem)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemInSlot != null &&
                (int)inventory[i].itemInSlot.itemDataType >= 3 &&
                inventory[i].itemInSlot.itemDataImage == newItem.itemDataImage)
            {
                inventory[i].itemInSlot = newItem;
                return i;
            }
        }

        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].isEmpty)
            {
                inventory[i].itemInSlot = newItem;
                return i;
            }
        }
        return -1;
    }
}