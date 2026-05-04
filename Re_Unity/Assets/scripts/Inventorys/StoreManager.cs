using UnityEngine;

public class StoreManager : MonoBehaviour
{
    [Header("UI Panel")]
    public GameObject storePanel;

    [Header("Scripts")]
    public Inventory storeData;
    public InventoryUI storeUI;
    private InventoryManager inventoryManager;

    private void Awake()
    {

        // 1. UI ��ũ��Ʈ���� �������� ����� ����
        storeUI.InitSlots();


        // 2. ������ UI ������ŭ ������ ��ũ��Ʈ���� ĭ�� ������ ����
        storeData.InitializeData(storeUI.inventoryUI.Count);

        storeData.inventory.Clear();
        foreach (SlotUI slot in storeUI.inventoryUI)
            storeData.inventory.Add(slot.slotData);
    }

    private void Start()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
        //storePanel.SetActive(false);
    }

    // 상점 열기/닫기
    public void ToggleStore()
    {
        bool isActive = !storePanel.activeSelf;
        storePanel.SetActive(isActive);
        Cursor.visible = isActive;
    }

    // 아이템 구매 (상점 → 인벤토리)
    public void BuyItem(ItemData item)
    {
        int result = inventoryManager.addItem_Button(item);
        if (result != -1)
            Debug.Log($"{item.name} 구매 성공!");
        else
            Debug.Log("구매 실패! 인벤토리가 가득 찼거나 무게 초과!");
    }

    // 아이템 판매 (인벤토리 → 상점)
    public void SellItem(ItemData item)
    {
        // 추후 구현
    }
}