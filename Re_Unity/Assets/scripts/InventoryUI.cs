using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("Slot Generation Settings")]
    public GameObject slotPrefab;   // ������Ʈ â�� ���� ������
    public Transform slotParent;    // ���̾��Ű�� InventoryPanel (Grid Layout Group)
    [Header("�κ��丮ĭ �� ���� ���� ����")]
    public int slotCount;      // ������ ���� ����
    [HideInInspector]
    public List<SlotUI> inventoryUI = new List<SlotUI>();

    // �������� ����� ������ ���� �����ϴ� ���� ���
    public void InitSlots()
    {
        if (slotPrefab == null || slotParent == null) return;

        inventoryUI.Clear();

        for (int i = 0; i < slotCount; i++)
        {
            // �θ� �ؿ� ������ ����
            GameObject newSlot = Instantiate(slotPrefab, slotParent);
            SlotUI uiComp = newSlot.GetComponent<SlotUI>();

            if (uiComp != null)
            {
                inventoryUI.Add(uiComp);
            }
        }
        Debug.Log($"���� {inventoryUI.Count}�� ���� �� UI ���� �Ϸ�!");
    }

    // UI ����
    public void UpdateSlotUI(int index, ItemSlot data)
    {
        if (index >= 0 && index < inventoryUI.Count)
        {
            inventoryUI[index].UpdateSlot(data);
        }
    }
}