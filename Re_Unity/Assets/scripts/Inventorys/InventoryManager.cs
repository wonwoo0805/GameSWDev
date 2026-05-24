using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    [Header("UI Panel")]
    public GameObject inventoryPanel;
    public GameObject equipmentPanel;

    [Header("UI Toggle feature")]
    public PlayerInput playerInput;

    [Header("Scripts")]
    public Inventory invData;
    public InventoryUI invUI;
    public Inventory eqptData;
    public InventoryUI eqptUI;

    [Header("defaultWeapon")]
    public ItemData defaultWeapon;

    [Header("Button")]
    public Button closeInventoryButton;
    public Button openInventoryButton;

    private void Awake()
    {
        //maintain InventoryInfo while changing scene
        DontDestroyOnLoad(transform.root.gameObject);

        //inisiate Inventory Slots(seperate UI and Data)
        invUI.InitSlots();
        invData.InitializeData(invUI.inventoryUI.Count);
        invData.inventory.Clear();
        foreach (SlotUI slot in invUI.inventoryUI)
            invData.inventory.Add(slot.slotData);
    }

    private void Start()
    {
        //for itemExchange with equipSlots
        SlotUI[] eqptSlots = equipmentPanel.GetComponentsInChildren<SlotUI>();
        foreach (SlotUI slot in eqptSlots)
            invData.equipment.Add(slot.slotData);

        inventoryPanel.SetActive(false);

        //occur events when click these buttons
        closeInventoryButton.onClick.AddListener(OnCloseButtonClick);
        openInventoryButton.onClick.AddListener(OnOpenButtonClick);
    }

    private void Update()
    {

    }

    public void OnToggleInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ToggleInventory();
        }

    }

    public void OnCloseInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CloseInventory();
        }

    }
    public void OnCloseButtonClick()
    {
        inventoryPanel.SetActive(false);
    }

    public void OnOpenButtonClick()
    {
        inventoryPanel.SetActive(true);
    }

    private void ToggleInventory()
    {
        if (inventoryPanel == null) return;

        inventoryPanel.SetActive(true); // ui active
        playerInput.SwitchCurrentActionMap("UI"); // change action map to ui
        Cursor.lockState = CursorLockMode.None; // change cursor state
        Cursor.visible = true;
    }

    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        playerInput.SwitchCurrentActionMap("Player");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public int addItem_Button(ItemData newData)
    {
        if (newData == null) return -1;
        
        int index = invData.addItem(newData);

        //judge addItem was successful
        if (index != -1)
        {
            invUI.UpdateSlotUI(index, invData.inventory[index]);
            return 1;
        }
        else
        {
            return -1;
        }
    }

    //to give itemData to weaponPreviewManager
    public ItemData GetEquippedItem(ItemType type)
    {
        //make temporary slots to check equipSlots
        SlotUI[] eqptSlots = equipmentPanel.GetComponentsInChildren<SlotUI>();
        //circuit every equipSlots
        foreach (SlotUI slot in eqptSlots)
        {
            if (slot.slotType == type && !slot.slotData.isEmpty)
                return slot.slotData.itemInSlot;
        }

        Debug.Log("have no weapon in weaponSlot");
        return defaultWeapon;
    }
}