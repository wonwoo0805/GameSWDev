using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("Slot Generation Settings")]
    public GameObject slotPrefab;
    public Transform slotParent;

    [Header("total inventorySlots")]
    public int slotCount;

    [HideInInspector]
    public List<SlotUI> inventoryUI = new List<SlotUI>();

    //clear all Images in inventory
    public void InitSlots()
    {
        if (slotPrefab == null || slotParent == null) return;

        foreach (SlotUI slot in inventoryUI)
            Destroy(slot.gameObject);

        inventoryUI.Clear();

        for (int i = 0; i < slotCount; i++)
        {
            GameObject newSlot = Instantiate(slotPrefab, slotParent);
            SlotUI uiComp = newSlot.GetComponent<SlotUI>();

            if (uiComp != null)
            {
                inventoryUI.Add(uiComp);
                uiComp.slotIndex = i;
            }
        }
    }

    public void UpdateSlotUI(int index, ItemSlot data)
    {
        if (index >= 0 && index < inventoryUI.Count)
        {
            inventoryUI[index].UpdateSlot(data);
        }
    }
}