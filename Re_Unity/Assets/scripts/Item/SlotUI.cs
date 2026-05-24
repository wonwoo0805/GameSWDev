using UnityEngine;
using UnityEngine.EventSystems; 
using UnityEngine.UI;

public class SlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
    [Header("How this slot can accomodate ItemType")]
    public ItemType slotType = ItemType.Any;

    [Header("UI Images")]
    public Image slotImage;    //slot backgroundImage
    public Image itemImage;    //itemImage in this slot

    [Header("etc")]
    public ItemSlot slotData;  //to get itemData which slot need
    private StorageManager storageManager;

    private static GameObject dragIcon;
    private Inventory inventory;

    protected void Awake()
    {
        if (slotData == null)
        {
            slotData = new ItemSlot();
            
        }
        inventory = FindAnyObjectByType<Inventory>();
        storageManager = FindAnyObjectByType<StorageManager>();
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        if (slotData.itemInSlot == null || slotData.itemInSlot == null) return;

        //make DragIcon
        dragIcon = new GameObject("DragIcon");
        dragIcon.transform.SetParent(GetComponentInParent<Canvas>().transform);
        //DragIcon = itemImage
        var img = dragIcon.AddComponent<Image>();
        img.sprite = itemImage.sprite;
        img.raycastTarget = false;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        //move dargIcon when move mouse
        if (dragIcon != null) dragIcon.transform.position = eventData.position;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        Destroy(dragIcon);
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        SlotUI startSlot = eventData.pointerDrag.GetComponent<SlotUI>();
        //check this drag is formed at slot
        if (startSlot != null)
        {  
            //check there is slot at first && (slot can accomodate any item || start slot and end slot's type is equal) && (end slot have no item || startslot can accmomodate any item || startslot can accmomodate endSlot's data)
            if ((startSlot.slotData.itemInSlot != null) &&
                (this.slotType == ItemType.Any || startSlot.slotData.itemInSlot.itemDataType == this.slotType) &&
                (this.slotData.itemInSlot == null || startSlot.slotType == ItemType.Any || this.slotData.itemInSlot.itemDataType == startSlot.slotType))
            {
                SwapItems(startSlot);
            }
        }
    }
    //swap slot at the aspect of UI(swap Image)
    public void SwapItems(SlotUI other)
    {
        ItemSlot temp = new ItemSlot(this.slotData.itemInSlot, this.slotData.quantity);

        this.UpdateSlot(new ItemSlot(other.slotData.itemInSlot, other.slotData.quantity));
        //swap substantial itemData)
        inventory.exchangeItemData(this, other);
        //update SlotImage
        other.UpdateSlot(temp);
    }

    public void UpdateSlot(ItemSlot newItem)
    {
        if (newItem == null)
        {
            slotData.itemInSlot = null;
            slotData.quantity = 0;
        }
        else
        {
            slotData.itemInSlot = newItem.itemInSlot;
            slotData.quantity = newItem.quantity;
        }

        if (slotData.itemInSlot != null)
        {
            itemImage.sprite = slotData.itemInSlot.itemDataImage;
            itemImage.enabled = true;
        }
        else
        {
            itemImage.sprite = null;
            itemImage.enabled = false; 
        }
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (storageManager == null) return;
        if (slotData.isEmpty) return;
        storageManager.SelectItem(slotData.itemInSlot, transform.GetSiblingIndex());
    }
}
