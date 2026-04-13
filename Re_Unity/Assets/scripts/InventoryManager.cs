using UnityEngine;
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

    private void Awake()
    {
        // 1. UI ��ũ��Ʈ���� �������� ����� ����
        invUI.InitSlots();
        

        // 2. ������ UI ������ŭ ������ ��ũ��Ʈ���� ĭ�� ������ ����
        invData.InitializeData(invUI.inventoryUI.Count);

        invData.inventory.Clear();
        foreach (SlotUI slot in invUI.inventoryUI)
            invData.inventory.Add(slot.slotData);
    }

    private void Start()
    {
        SlotUI[] eqptSlots = equipmentPanel.GetComponentsInChildren<SlotUI>();
        foreach (SlotUI slot in eqptSlots)
            invData.equipment.Add(slot.slotData);
        inventoryPanel.SetActive(false);
    }

    private void Update()
    {
        // 2. Keyboard.current�� ����Ͽ� 'I' Ű �Է��� ����
        // wasPressedThisFrame�� Ű�� '�� ������ �� �� ��'�� ����ǰ� �մϴ�.
        /*
        if (Keyboard.current != null && Keyboard.current.iKey.wasPressedThisFrame)
        {
            ToggleInventory();
        }
        */
    }

    public void OnToggleInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ToggleInventory();
        }
        
        //Debug.Log("Check");
    }

    public void OnCloseInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CloseInventory();
        }
        
        //Debug.Log("Check");
    }

    private void ToggleInventory()
    {
        if (inventoryPanel == null) return;
        inventoryPanel.SetActive(true); // ui active
        playerInput.SwitchCurrentActionMap("UI"); // change action map to ui
        Cursor.lockState = CursorLockMode.None; // change cursor state
        Cursor.visible = true;
    }

    private void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        playerInput.SwitchCurrentActionMap("Player");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // ���� �Լ��� ���� (��ư �̺�Ʈ �����)
    public int addItem_Button(ItemData newData)
    {
        Debug.Log($"inventoryPanel: {inventoryPanel}");
        Debug.Log($"activeSelf: {inventoryPanel?.activeSelf}");
        /*
        if (inventoryPanel == null || !inventoryPanel.activeSelf)
        {
            Debug.Log("�κ��丮 â�� ���� �־� �������� �߰��� �� �����ϴ�.");
            return -1;
        }
        */

        if (newData == null) return -1;
        
        int index = invData.addItem(newData);

        if (index != -1)
        {
            invUI.UpdateSlotUI(index, invData.inventory[index]);
            Debug.Log("�߰� ����!");
            return 1;
        }
        else
        {
            Debug.Log("�κ��丮�� ���� á���ϴ�.");
            return -1;
        }
    }
}