using NUnit.Framework;
using System.Collections.Generic;
using System.Xml;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    [Header("UI Panel")]
    public GameObject storePanel;

    [Header("Scripts")]
    public Inventory storeData;
    public InventoryUI storeUI;
    private InventoryManager inventoryManager;

    [Header("아이템 풀")]
    public List<ItemData> itemPool;
    public int totalItemCount;

    private int normal = 50;
    private int rare = 30;
    private int epic = 15;
    private int unique = 4;
    private int legendary = 1;

    private void Awake()
    {
        ItemData[] loadedItems = Resources.LoadAll<ItemData>("ItemData");
        itemPool.AddRange(loadedItems);
        totalItemCount = loadedItems.Length;
        Debug.Log($"{totalItemCount}개");

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
        for (int i = 0; i < storeData.inventory.Count; i++)
            RegisterItemToSlot(i);
    }

    private (ItemRarity rarity, int code) DrawRarityAndCode()
    {
        // 등급 추첨
        int rarityRoll = Random.Range(1, 101);
        ItemRarity selectedRarity;

        if (rarityRoll <= normal)
            selectedRarity = ItemRarity.Normal;
        else if (rarityRoll <= normal + rare)
            selectedRarity = ItemRarity.Rare;
        else if (rarityRoll <= normal + rare + epic)
            selectedRarity = ItemRarity.Epic;
        else if (rarityRoll <= normal + rare + epic + unique)
            selectedRarity = ItemRarity.Unique;
        else
            selectedRarity = ItemRarity.Legendary;

        // 아이템 코드 추첨
        int code = Random.Range(0, totalItemCount);

        return (selectedRarity, code);
    }

    // 아이템을 가져와 슬롯에 등록하는 함수
    private void RegisterItemToSlot(int slotIndex)
    {
        int maxAttempts = 100; // 이거 다돌면 당신은 행운아

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            var (rarity, code) = DrawRarityAndCode();
            Debug.Log($"추첨 결과 - rarity: {rarity}, code: {code}");

            // 이미 상점에 등록된 아이템인지 확인
            bool isDuplicate = storeData.inventory.Exists(
                slot => slot.itemInSlot != null &&
                        slot.itemInSlot.itemDataRarity == rarity &&
                        slot.itemInSlot.itemDataCode == code
            );
            if (isDuplicate)
            {
                Debug.Log("중복! 다시 추첨");
                continue;
            }

            // 레어리티와 코드가 일치하는 아이템 탐색
            ItemData foundItem = itemPool.Find(
                item => item.itemDataRarity == rarity &&
                        item.itemDataCode == code
            );
            Debug.Log($"찾은 아이템: {foundItem?.name}");

            if (foundItem == null) continue;

            // 슬롯에 등록
            storeData.inventory[slotIndex].itemInSlot = foundItem;
            storeUI.UpdateSlotUI(slotIndex, storeData.inventory[slotIndex]);
            return;
        }
        Debug.Log($"슬롯 {slotIndex}에 등록할 아이템을 찾지 못했습니다!");
    }
    public void SetRarityChances(int newNormal, int newRare, int newEpic, int newUnique, int newLegendary)
    {
        normal = newNormal;
        rare = newRare;
        epic = newEpic;
        unique = newUnique;
        legendary = newLegendary;
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
        //상점에 있던 아이템을 없애는 기능
        else
            Debug.Log("구매 실패! 인벤토리가 가득 찼거나 무게 초과!");
    }

    // 아이템 판매 (인벤토리 → 상점)
    public void SellItem(ItemData item)
    {
        // 추후 구현
    }
}