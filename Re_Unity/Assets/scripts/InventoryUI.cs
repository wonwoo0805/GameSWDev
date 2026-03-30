using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("Slot Generation Settings")]
    public GameObject slotPrefab;   // 프로젝트 창의 슬롯 프리팹
    public Transform slotParent;    // 하이어라키의 InventoryPanel (Grid Layout Group)
    [Header("인벤토리칸 총 개수 직접 설정")]
    public int slotCount;      // 생성할 슬롯 개수
    [HideInInspector]
    public List<SlotUI> inventoryUI = new List<SlotUI>();

    // 프리팹을 사용해 슬롯을 직접 생성하는 기존 방식
    public void InitSlots()
    {
        if (slotPrefab == null || slotParent == null) return;

        inventoryUI.Clear();

        for (int i = 0; i < slotCount; i++)
        {
            // 부모 밑에 프리팹 생성
            GameObject newSlot = Instantiate(slotPrefab, slotParent);
            SlotUI uiComp = newSlot.GetComponent<SlotUI>();

            if (uiComp != null)
            {
                inventoryUI.Add(uiComp);
            }
        }
        Debug.Log($"슬롯 {inventoryUI.Count}개 생성 및 UI 연결 완료!");
    }

    // UI 갱신
    public void UpdateSlotUI(int index, ItemSlot data)
    {
        if (index >= 0 && index < inventoryUI.Count)
        {
            inventoryUI[index].UpdateSlot(data);
        }
    }
}