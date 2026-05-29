using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        closeInventoryButton.onClick.AddListener(CloseInventory);
        openInventoryButton.onClick.AddListener(ToggleInventory);
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
        
        Cursor.lockState = CursorLockMode.None; // change cursor state
        Cursor.visible = true;

        playerInput.SwitchCurrentActionMap("UI");
        //StartCoroutine(SafeSwitchActionMap("UI"));
    }

    public void CloseInventory()
    {
        if (inventoryPanel == null) return;

        Debug.Log("asdfahsdfjkh");
        inventoryPanel.SetActive(false);

        

        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            playerInput.SwitchCurrentActionMap("Player");
            //StartCoroutine(SafeSwitchActionMap("Player"));
        }
            
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
        SlotUI targetSlot = null;
        //make temporary slots to check equipSlots
        SlotUI[] eqptSlots = equipmentPanel.GetComponentsInChildren<SlotUI>();
        //circuit every equipSlots
        foreach (SlotUI slot in eqptSlots)
        {
            if (slot.slotType == type)
            {
                targetSlot = slot;
                if(!slot.slotData.isEmpty)
                {
                    return slot.slotData.itemInSlot;
                }
            }
        }

        if(type == ItemType.Weapon)
        {
            Debug.Log("have no weapon in weaponSlot");
            targetSlot.slotData.itemInSlot = defaultWeapon;
            targetSlot.UpdateSlot(targetSlot.slotData);
            return defaultWeapon;
        }

        return null;
    }
    public void sellRefunds()
    {
        foreach (SlotUI slot in invUI.inventoryUI)
        {
            if (slot.slotData.isEmpty) continue;
            ItemData item = slot.slotData.itemInSlot;

            if (item.itemDataType == ItemType.Refund)
            {
                invData.totalMoney += item.ItemDataMoney;
                slot.UpdateSlot(null);
            }
        }
    }
}