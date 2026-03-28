using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    //[Header("inventorySizeSetting")]
    //public int width = 5;
    //public int height = 4;

    [Header("link to object")]
    [SerializeField] private GameObject slotPrefab; // 프로젝트 창의 슬롯 프리팹
    [SerializeField] private Transform slotParent;  // 하이어라키의 InventoryPanel (Grid Layout Group)


    public GameObject inventoryPanel;
    public GameObject equipmentPanel;
    [Header("realItemData")]
    // 실제 아이템 데이터가 담기는 리스트
    public List<ItemSlot> inventoryData = new List<ItemSlot>();
    private List<ItemSlot> equipmentData = new List<ItemSlot>();
    // 생성된 각 슬롯의 UI 컴포넌트를 담는 리스트
    public List<SlotUI> inventoryUI = new List<SlotUI>();
    private List<SlotUI> equipmentUI = new List<SlotUI>();

    private void Awake()
    {
        InitSlots(inventoryPanel, inventoryUI, inventoryData);

        // 장비 슬롯들 초기화 (패널만 연결되어 있다면 똑같이 작동!)
        InitSlots(equipmentPanel, equipmentUI, equipmentData);


        
    }

    void InitSlots(GameObject panel, List<SlotUI> uiList, List<ItemSlot> dataList)
    {
        //if (panel == null) return;

        SlotUI[] slots = panel.GetComponentsInChildren<SlotUI>(true);
        uiList.AddRange(slots);

        for (int i = 0; i < uiList.Count; i++)
        {
            dataList.Add(ScriptableObject.CreateInstance<ItemSlot>());
        }

        Debug.Log("데이터와 UI 연결 완료!");
        Debug.Log($"찾은 UI 슬롯 개수: {slots.Length}");
    }

    public bool addItem(ItemData newItem)
    {
        for(int i = 0; i < inventoryData.Count; i++)
        {
            //canOverlap
            if (inventoryData[i].itemInSlot != null && (int)inventoryData[i].itemInSlot.itemDataType >= 3 && inventoryData[i].itemInSlot.itemDataImage == newItem.itemDataImage)
            {
                //change item in slot
                inventoryData[i].itemInSlot = newItem;
                //update inventoryUI
                inventoryUI[i].UpdateSlot(inventoryData[i]);

                return true;
            }
        }
        
        //isEmpty
        for (int i = 0; i < inventoryData.Count; i++)
        {
            //isEmpty or canOverlap
            if (inventoryData[i].isEmpty)
            {
                //change item in slot
                inventoryData[i].itemInSlot = newItem;
                //update inventoryUI
                inventoryUI[i].UpdateSlot(inventoryData[i]);

                return true;
            }
        }
        
        return false;
    }

    public void addItem_Button(ItemData newData)
    {
        bool result = addItem(newData);

        if (result) Debug.Log("추가 성공!");
        else Debug.Log($"인벤토리가 가득 찼습니다.");

    }
}
