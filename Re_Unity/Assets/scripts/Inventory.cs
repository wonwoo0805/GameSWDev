using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemSlot> inventory = new List<ItemSlot>();
    public List<ItemSlot> equipment = new List<ItemSlot>();

    // 지정된 개수만큼 데이터 빈 칸 만들기
    public void InitializeData(int size)
    {
        inventory.Clear();
        for (int i = 0; i < size; i++)
            inventory.Add(new ItemSlot());
        
    }
    //장비칸은 배열을 따로 지정
    

    public int addItem(ItemData newItem)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            //현재 인벤토리에 들어있는 아이템이 들어왔다면 개수를 중첩
            if (inventory[i].itemInSlot != null &&
                (int)inventory[i].itemInSlot.itemDataType >= 4 &&
                inventory[i].itemInSlot.itemDataImage == newItem.itemDataImage)
            {
                inventory[i].itemInSlot = newItem;
                //아이템 수량을 중첩시키는 부분(미구현)
                inventory[i].quantity += newItem.itemDataNum;
                return i;
            }
        }

        //중첩이 가능하지 않다면 가능한 빈칸 중 맨 앞(왼쪽 위 기준)에 아이템 추가
        for (int i = 0; i < inventory.Count; i++)
        {
            Debug.Log($"inventory[{i}]: {inventory[i].itemInSlot?.name}, isEmpty: {inventory[i].isEmpty}, isNull: {inventory[i].itemInSlot == null}");
            if (inventory[i].isEmpty)
            {
                inventory[i].itemInSlot = newItem;
                inventory[i].quantity += newItem.itemDataNum;
                return i;
            }
        }
        return -1;
    }

    public void exchangeItemData(SlotUI startSlot, SlotUI endSlot)
{
    List<ItemSlot> startList = inventory.Contains(startSlot.slotData) ? inventory : equipment;
    List<ItemSlot> endList = inventory.Contains(endSlot.slotData) ? inventory : equipment;

    int startIdx = startList.IndexOf(startSlot.slotData);
    int endIdx = endList.IndexOf(endSlot.slotData);

    if (startIdx < 0 || endIdx < 0) return;

    ItemData tempItem = startList[startIdx].itemInSlot;

    startList[startIdx].itemInSlot = endList[endIdx].itemInSlot;

    endList[endIdx].itemInSlot = tempItem;

    // 교환 후 상태 확인
    Debug.Log($"교환 후 startList[{startIdx}]: {startList[startIdx].itemInSlot?.name}");
    Debug.Log($"교환 후 endList[{endIdx}]: {endList[endIdx].itemInSlot?.name}");
}
}