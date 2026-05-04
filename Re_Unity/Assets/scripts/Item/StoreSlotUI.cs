using UnityEngine.EventSystems;

public class StoreSlotUI : SlotUI, IPointerClickHandler
{
    private StoreManager storeManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        storeManager.BuyItem(slotData.itemInSlot);
    }
}