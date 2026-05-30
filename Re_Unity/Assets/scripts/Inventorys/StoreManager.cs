using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class StoreManager : MonoBehaviour
{
    [Header("UI Panel")]
    public GameObject storePanel;

    [Header("Scripts")]
    public Inventory storeData;
    public InventoryUI storeUI;
    private InventoryManager inventoryManager;
    private ItemExplainPanel itemExplainPanel;

    [Header("Item Pool")]
    public List<ItemData> itemPool;
    public int totalItemCount;
    public ItemTable storeItemTable;

    [Header("Rarity Percentage")]
    private int normal = 50;
    private int rare = 30;
    private int epic = 15;
    private int unique = 4;
    private int legendary = 1;

    [Header("Button")]
    public Button buyButton;
    public Button rerollButton;
    public Button closeStoreButton;
    public Button openStoreButton;

    private ItemData selectedItem;
    private int selectedSlotIndex;

    public static StoreManager Instance;
    public event Action OnGoldChanged;

    private void Awake()
    {
        //maintain StoreInfo while changing scene
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            Destroy(transform.root.gameObject);
        }

        //get itemPool for storeItemList
        //ItemData[] loadedItems = Resources.LoadAll<ItemData>("ItemData").Where(item => item.itemDataType != ItemType.Refund).ToArray();
        //itemPool.AddRange(loadedItems);
        //totalItemCount = loadedItems.Length;
        storeItemTable.Initialize(ItemType.Refund);

        storeUI.InitSlots();
        storeData.InitializeData(storeUI.inventoryUI.Count);

        storeData.inventory.Clear();
        foreach (SlotUI slot in storeUI.inventoryUI)
            storeData.inventory.Add(slot.slotData);
    }

    private void Start()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
        itemExplainPanel = FindAnyObjectByType<ItemExplainPanel>();

        storePanel.gameObject.SetActive(false);

        //occur events when click these buttons 
        buyButton.onClick.AddListener(OnBuyButtonClick);
        rerollButton.onClick.AddListener(OnRerollButtonClick);
        closeStoreButton.onClick.AddListener(OnCloseButtonClick);
        openStoreButton.onClick.AddListener(OnOpenButtonClick);
        
        //register items on storeSlot at first
        for (int i = 0; i < storeData.inventory.Count; i++)
            RegisterItemToSlot(i);
    }
    //draw item randomly
    private (ItemRarity rarity, int code) DrawRarityAndCode()
    {
        //draw itemRarity
        int rarityRoll = UnityEngine.Random.Range(1, 101);
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

        //draw itemCode
        int code = UnityEngine.Random.Range(0, totalItemCount);

        return (selectedRarity, code);
    }

    private void RegisterItemToSlot(int slotIndex)
    {
        int maxAttempts = 100;
        
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            ItemData foundItem = storeItemTable.GetRandomItem();

            //check this item is exist in any storeSlots
            bool isDuplicate = storeData.inventory.Exists(
                slot => slot.itemInSlot != null &&
                        slot.itemInSlot.itemDataCode == foundItem.itemDataCode
            );
            if (isDuplicate)
            {
                continue;
            }

            //find item with code and rarity
            /*
            ItemData foundItem = itemPool.Find(
                item => item.itemDataRarity == rarity &&
                        item.itemDataCode == code
            );
            */

            //if can't find item , 
            if (foundItem == null) continue;

            //reigister choosed item to slot
            storeData.inventory[slotIndex].itemInSlot = foundItem;
            storeUI.UpdateSlotUI(slotIndex, storeData.inventory[slotIndex]);
            return;
        }
    }
    //set item rarity percentage
    public void SetRarityChances(int newNormal, int newRare, int newEpic, int newUnique, int newLegendary)
    {
        normal = newNormal;
        rare = newRare;
        epic = newEpic;
        unique = newUnique;
        legendary = newLegendary;
    }

    // store open/close
    public void ToggleStore()
    {
        //get opposite condition with panel's activity
        bool isActive = !storePanel.activeSelf;
        storePanel.SetActive(isActive);
        Cursor.visible = isActive;
    }
    public void SelectItem(ItemData item, int slotIndex)
    {
        selectedItem = item;
        selectedSlotIndex = slotIndex;
        //show item's description
        itemExplainPanel.ShowDescription(item);
        //activate "purchase" button
        buyButton.gameObject.SetActive(true);
    }
    private void OnBuyButtonClick()
    {
        if (selectedItem == null) return;
        BuyItem(selectedItem, selectedSlotIndex);
        buyButton.gameObject.SetActive(false);
        selectedItem = null;
    }

    // 아이템 구매 (상점 → 인벤토리)
    public void BuyItem(ItemData item, int slotIndex)
    {
        if(item.ItemDataMoney > inventoryManager.invData.totalMoney) 
        {
            Debug.Log("not enough gold");
            return;
        }

        int result = inventoryManager.addItem_Button(item);
        //check additem is successful
        if (result != -1)
        {
            storeData.inventory[slotIndex].itemInSlot = null;
            storeUI.UpdateSlotUI(slotIndex, storeData.inventory[slotIndex]);
            inventoryManager.invData.totalMoney -= item.ItemDataMoney;
            OnGoldChanged?.Invoke();
        }
        else
            //if this player exist
            Debug.Log("구매 실패! 인벤토리가 가득 찼거나 무게 초과!");
    }

    public void SellItem(ItemData item)
    {
        
    }
    public void OnRerollButtonClick()
    {
        //clear all items in slot
        for (int i = 0; i < storeData.inventory.Count; i++)
        {
            storeData.inventory[i].itemInSlot = null;
            storeUI.UpdateSlotUI(i, storeData.inventory[i]);
        }

        //clear selected item, buyButton, description
        selectedItem = null;
        buyButton.gameObject.SetActive(false);
        itemExplainPanel.ShowDescription(null);

        //fill items to slots
        for (int i = 0; i < storeData.inventory.Count; i++)
            RegisterItemToSlot(i);
    }

    public void OnOpenButtonClick()
    {
        storePanel.SetActive(true);
        buyButton.gameObject.SetActive(false);
    }
    public void OnCloseButtonClick()
    {
        storePanel.SetActive(false);
    }
}