using UnityEngine;
using UnityEngine.UI;
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

    [Header("Inventory Size")]
    public Vector2 normalCellsize;
    public Vector2 storageCellSize;

    private ItemData selectedItem;
    private int selectedSlotIndex;

    private void Awake()
    {
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
        storageMasterPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenStorage()
    {
        if (inventoryManager.inventoryPanel.activeSelf)
            inventoryManager.CloseInventory();

        inventoryOriginalParent = inventoryPanel.transform.parent;
        inventoryPanel.transform.SetParent(storageMasterPanel.transform, false);

        RectTransform invRect = inventoryPanel.GetComponent<RectTransform>();
        invRect.anchorMin = new Vector2(0, 0.3f);
        invRect.anchorMax = new Vector2(0.5f, 1f);
        invRect.offsetMin = Vector2.zero;
        invRect.offsetMax = Vector2.zero;

        inventoryGrid.cellSize = storageCellSize;
        storageMasterPanel.SetActive(true);
    }

    public void CloseStorage()
    {
        inventoryPanel.transform.SetParent(inventoryOriginalParent);

        RectTransform invRect = inventoryPanel.GetComponent<RectTransform>();
        invRect.anchorMin = new Vector2(0, 0);
        invRect.anchorMax = new Vector2(1f, 1f);
        invRect.offsetMin = Vector2.zero;
        invRect.offsetMax = new Vector2(-380f, 0f);

        
        inventoryGrid.cellSize = normalCellsize;
        storageMasterPanel.SetActive(false);
    }

    public void SelectItem(ItemData item, int slotIndex)
    {
        selectedItem = item;
        selectedSlotIndex = slotIndex;
        storageExplainPanel.ShowDescription(item);
    }
}
