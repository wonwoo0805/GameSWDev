using UnityEngine;
using UnityEngine.UI;
using System;
using static UnityEditor.Progress;
using static UnityEngine.Analytics.IAnalytic;

public class StorageManager : MonoBehaviour
{
    [Header("StorageScript")]
    public Inventory storageData;
    public InventoryUI storageUI;

    [Header("UI Panel")]
    public GameObject storageMasterPanel;
    public GameObject storagePanel;
    public GameObject inventoryPanel;
    public ItemExplainPanel storageExplainPanel;

    [Header("Inventory Tab")]
    public Transform inventoryOriginalParent;
    public GridLayoutGroup inventoryGrid;
    private InventoryManager inventoryManager;

    [Header("Button")]
    public Button openButton;
    public Button closeButton;
    public Button sellButton;

    [Header("Inventory Size")]
    public Vector2 normalCellsize;
    public Vector2 storageCellSize;

    private ItemData selectedItem;
    private int selectedSlotIndex;

    public event Action OnGoldChanged;
    public static StorageManager Instance;

    //Inisiate slots in Store
    private void Awake()
    {
        //maintain StorageInfo while changing scene
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            Destroy(transform.root.gameObject);
        }

        storageUI.InitSlots();

        storageData.InitializeData(storageUI.inventoryUI.Count);

        storageData.inventory.Clear();
        foreach (SlotUI slot in storageUI.inventoryUI)
            storageData.inventory.Add(slot.slotData);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
        openButton.onClick.AddListener(OpenStorage);
        closeButton.onClick.AddListener(CloseStorage);
        sellButton.onClick.AddListener(SellItem);
        storageMasterPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Open storageUI
    public void OpenStorage()
    {
        if (storageMasterPanel.activeSelf) return;
        //Need fix not to able other UI button while UI is able
        if (inventoryManager.inventoryPanel.activeSelf)
            inventoryManager.CloseInventory();

        //Give inventorypanel to storage
        inventoryOriginalParent = inventoryPanel.transform.parent;
        inventoryPanel.transform.SetParent(storageMasterPanel.transform, false);
        
        //Set inventorysize to storage
        RectTransform invRect = inventoryPanel.GetComponent<RectTransform>();
        invRect.anchorMin = new Vector2(0, 0.416f);
        invRect.anchorMax = new Vector2(0.5f, 1f);
        invRect.offsetMin = Vector2.zero;
        invRect.offsetMax = Vector2.zero;

        //set inventorycellsize to storage
        inventoryGrid.cellSize = storageCellSize;
        storagePanel.SetActive(true);
        storageMasterPanel.SetActive(true);
    }

    //Close storageUI
    public void CloseStorage()
    {
        //give inventoryPanel to inventoryUI
        inventoryPanel.transform.SetParent(inventoryOriginalParent, false);
        
        //set inventoryPanelSize to inventoryUI;
        RectTransform invRect = inventoryPanel.GetComponent<RectTransform>();
        invRect.anchorMin = new Vector2(0, 0);
        invRect.anchorMax = new Vector2(0.6111f, 1f);
        invRect.offsetMin = Vector2.zero;
        invRect.offsetMax = Vector2.zero;

        //set inventorycellsize to inventoryUI
        inventoryGrid.cellSize = normalCellsize;

        storageMasterPanel.SetActive(false);
    }

    //When Click storeItem
    public void SelectItem(ItemData item, int slotIndex)
    {
        //set for buying item and show discription
        selectedItem = item;
        selectedSlotIndex = slotIndex;
        storageExplainPanel.ShowDescription(item);
        sellButton.gameObject.SetActive(true);
    }

    public void SellItem()
    {
        inventoryManager.invData.totalMoney += selectedItem.ItemDataSellMoney;
        inventoryManager.invUI.inventoryUI[selectedSlotIndex].UpdateSlot(null);

        OnGoldChanged?.Invoke();
        sellButton.gameObject.SetActive(false);
    }
}
