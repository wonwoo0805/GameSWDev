using UnityEngine.EventSystems;

public class StoreSlotUI : SlotUI, IPointerClickHandler
{
    private StoreManager storeManager;
    private ItemExplainPanel itemExplainPanel;

    private void Awake()
    {
        base.Awake();
        storeManager = FindAnyObjectByType<StoreManager>();
        itemExplainPanel = FindAnyObjectByType<ItemExplainPanel>();
    }

    public override void OnBeginDrag(PointerEventData eventData) { }
    public override void OnDrag(PointerEventData eventData) { }
    public override void OnEndDrag(PointerEventData eventData) { }
    public override void OnDrop(PointerEventData eventData) { }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (slotData.isEmpty) return;
        storeManager.SelectItem(slotData.itemInSlot, transform.GetSiblingIndex());
    }
}