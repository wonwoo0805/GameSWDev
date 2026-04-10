using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemSlot> inventory = new List<ItemSlot>();
    public List<ItemSlot> equipment = new List<ItemSlot>();

    // ������ ������ŭ ������ �� ĭ �����
    public void InitializeData(int size)
    {
        inventory.Clear();
        for (int i = 0; i < size; i++)
            inventory.Add(new ItemSlot());
        
    }
    //���ĭ�� �迭�� ���� ����
    

    public int addItem(ItemData newItem)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            //���� �κ��丮�� ����ִ� �������� ���Դٸ� ������ ��ø
            if (inventory[i].itemInSlot != null &&
                (int)inventory[i].itemInSlot.itemDataType >= 4 &&
                inventory[i].itemInSlot.itemDataImage == newItem.itemDataImage)
            {
                inventory[i].itemInSlot = newItem;
                //������ ������ ��ø��Ű�� �κ�(�̱���)
                inventory[i].quantity += newItem.itemDataNum;
                Debug.Log("중첩가능");
                return i;
            }
        }

        //��ø�� �������� �ʴٸ� ������ ��ĭ �� �� ��(���� �� ����)�� ������ �߰�
        for (int i = 0; i < inventory.Count; i++)
        {
            Debug.Log($"inventory[{i}]: {inventory[i].itemInSlot?.name}, isEmpty: {inventory[i].isEmpty}, isNull: {inventory[i].itemInSlot == null}");
            if (inventory[i].isEmpty)
            {
                inventory[i].itemInSlot = newItem;
                inventory[i].quantity += newItem.itemDataNum;
                Debug.Log("그냥됨");
                return i;
            }
        }
        Debug.Log("아이고난");
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

    // ��ȯ �� ���� Ȯ��
    Debug.Log($"��ȯ �� startList[{startIdx}]: {startList[startIdx].itemInSlot?.name}");
    Debug.Log($"��ȯ �� endList[{endIdx}]: {endList[endIdx].itemInSlot?.name}");
}
}