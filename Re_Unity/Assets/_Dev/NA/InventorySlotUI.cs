using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotUI : SlotUI
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (slotData.isEmpty) return;

        // 창고가 열려 있으면 → 창고 고정 패널
        if (StorageManager.Instance != null &&
            StorageManager.Instance.storageMasterPanel.activeSelf)
        {
            StorageManager.Instance.SelectItem(slotData.itemInSlot, transform.GetSiblingIndex());
        }
        else  // 인벤토리 단독 → 아이템 위 툴팁
        {
            InventoryTooltip.Instance.ShowAt(slotData.itemInSlot, transform.position);
        }
    }
}