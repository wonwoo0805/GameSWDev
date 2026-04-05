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
        // 유니티 시스템이 완전히 준비된 Awake 시점에 생성자를 호출합니다.
        if (slotData == null)
        {
            slotData = new ItemSlot();
            
        }
        inventory = FindAnyObjectByType<Inventory>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slotData.itemInSlot == null || slotData.itemInSlot == null) return;

        // 드래그용 가짜 아이콘 생성
        dragIcon = new GameObject("DragIcon");
        dragIcon.transform.SetParent(GetComponentInParent<Canvas>().transform);
        var img = dragIcon.AddComponent<Image>();
        img.sprite = itemImage.sprite;
        img.raycastTarget = false; // ★중요: 마우스 클릭이 이걸 통과해서 아래 슬롯에 닿아야 함
    }

    // 2. 드래그 중
    public void OnDrag(PointerEventData eventData)
    {
        if (dragIcon != null) dragIcon.transform.position = eventData.position;
    }

    // 3. 드래그 끝
    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(dragIcon);
    }

    // 4. 아이템이 이 슬롯에 놓였을 때 (핵심 로직)
    public void OnDrop(PointerEventData eventData)
    {
        // 드래그해온 시작점 슬롯을 가져옴
        SlotUI startSlot = eventData.pointerDrag.GetComponent<SlotUI>();

        if (startSlot != null)
        {
            // [조건 체크] 이 슬롯이 요구하는 타입과 아이템 타입이 맞는지 확인
            if ((startSlot.slotData.itemInSlot != null) &&
                (this.slotType == ItemType.Any || startSlot.slotData.itemInSlot.itemDataType == this.slotType) &&
                (this.slotData.itemInSlot == null || startSlot.slotType == ItemType.Any || this.slotData.itemInSlot.itemDataType == startSlot.slotType))
            {
                // 데이터 교환(Swap) 로직 실행
                SwapItems(startSlot);
            }
        }
    }

    public void SwapItems(SlotUI other)
    {
        

        // 1. 데이터 교환 (값 복사 또는 참조 교환)
        ItemSlot temp = new ItemSlot(this.slotData.itemInSlot, this.slotData.quantity);

        // 내 슬롯을 상대방 데이터로 업데이트
        this.UpdateSlot(new ItemSlot(other.slotData.itemInSlot, other.slotData.quantity));
        inventory.exchangeItemData(this, other);
        // 상대방 슬롯을 내 데이터(temp)로 업데이트
        other.UpdateSlot(temp);

        
        
    }

    public void UpdateSlot(ItemSlot newItem)
    {
        // 1. 전달받은 새로운 데이터로 내 주머니(slotData)를 교체합니다.
        // newItem 자체가 null일 경우를 대비해 안전장치를 둡니다.
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

        // 2. 이제 별도의 변수 없이 '내 데이터'만 보고 UI를 결정합니다.
        // 슬롯에 아이템 데이터가 들어있다면?
        if (slotData.itemInSlot != null)
        {
            // 내 데이터에 저장된 이미지를 내 이미지 컴포넌트에 바로 넣습니다.
            itemImage.sprite = slotData.itemInSlot.itemDataImage;
            itemImage.enabled = true; // 아이콘 보이기
        }
        // 슬롯이 비어있다면?
        else
        {
            itemImage.sprite = null;
            itemImage.enabled = false; // 아이콘 숨기기 (이게 없어서 잔상이 남았던 것!)
        }
    }

}
