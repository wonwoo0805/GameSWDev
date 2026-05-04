using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class SlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    
    public ItemType slotType = ItemType.Any;
    public Image slotImage;    //slot background
    public Image itemImage;
    public ItemSlot slotData;

    private static GameObject dragIcon;

    private Inventory inventory;
    private void Awake()
    {
        // ����Ƽ �ý����� ������ �غ�� Awake ������ �����ڸ� ȣ���մϴ�.
        if (slotData == null)
        {
            slotData = new ItemSlot();
            
        }
        inventory = FindAnyObjectByType<Inventory>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slotData.itemInSlot == null || slotData.itemInSlot == null) return;

        // �巡�׿� ��¥ ������ ����
        dragIcon = new GameObject("DragIcon");
        dragIcon.transform.SetParent(GetComponentInParent<Canvas>().transform);
        var img = dragIcon.AddComponent<Image>();
        img.sprite = itemImage.sprite;
        img.raycastTarget = false; // ���߿�: ���콺 Ŭ���� �̰� ����ؼ� �Ʒ� ���Կ� ��ƾ� ��
    }

    // 2. �巡�� ��
    public void OnDrag(PointerEventData eventData)
    {
        if (dragIcon != null) dragIcon.transform.position = eventData.position;
    }

    // 3. �巡�� ��
    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(dragIcon);
    }

    // 4. �������� �� ���Կ� ������ �� (�ٽ� ����)
    public void OnDrop(PointerEventData eventData)
    {
        // �巡���ؿ� ������ ������ ������
        SlotUI startSlot = eventData.pointerDrag.GetComponent<SlotUI>();

        if (startSlot != null)
        {
            // [���� üũ] �� ������ �䱸�ϴ� Ÿ�԰� ������ Ÿ���� �´��� Ȯ��
            if ((startSlot.slotData.itemInSlot != null) &&
                (this.slotType == ItemType.Any || startSlot.slotData.itemInSlot.itemDataType == this.slotType) &&
                (this.slotData.itemInSlot == null || startSlot.slotType == ItemType.Any || this.slotData.itemInSlot.itemDataType == startSlot.slotType))
            {
                // ������ ��ȯ(Swap) ���� ����
                SwapItems(startSlot);
            }
        }
    }

    public void SwapItems(SlotUI other)
    {
        

        // 1. ������ ��ȯ (�� ���� �Ǵ� ���� ��ȯ)
        ItemSlot temp = new ItemSlot(this.slotData.itemInSlot, this.slotData.quantity);

        // �� ������ ���� �����ͷ� ������Ʈ
        this.UpdateSlot(new ItemSlot(other.slotData.itemInSlot, other.slotData.quantity));
        inventory.exchangeItemData(this, other);
        // ���� ������ �� ������(temp)�� ������Ʈ
        other.UpdateSlot(temp);

        
        
    }

    public void UpdateSlot(ItemSlot newItem)
    {
        // 1. ���޹��� ���ο� �����ͷ� �� �ָӴ�(slotData)�� ��ü�մϴ�.
        // newItem ��ü�� null�� ��츦 ����� ������ġ�� �Ӵϴ�.
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

        // 2. ���� ������ ���� ���� '�� ������'�� ���� UI�� �����մϴ�.
        // ���Կ� ������ �����Ͱ� ����ִٸ�?
        if (slotData.itemInSlot != null)
        {
            // �� �����Ϳ� ����� �̹����� �� �̹��� ������Ʈ�� �ٷ� �ֽ��ϴ�.
            itemImage.sprite = slotData.itemInSlot.itemDataImage;
            itemImage.enabled = true; // ������ ���̱�
        }
        // ������ ����ִٸ�?
        else
        {
            itemImage.sprite = null;
            itemImage.enabled = false; // ������ ����� (�̰� ��� �ܻ��� ���Ҵ� ��!)
        }
    }

}
