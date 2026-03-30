using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    [Header("UI Panel")]
    public GameObject inventoryPanel;

    [Header("Scripts")]
    public Inventory invData;
    public InventoryUI invUI;

    private void Awake()
    {
        // 1. UI 스크립트에게 프리팹을 찍어내라고 명령
        invUI.InitSlots();

        // 2. 생성된 UI 개수만큼 데이터 스크립트에게 칸을 만들라고 명령
        invData.InitializeData(invUI.inventoryUI.Count);
    }

    private void Update()
    {
        // 2. Keyboard.current를 사용하여 'I' 키 입력을 감지
        // wasPressedThisFrame은 키를 '딱 눌렀을 때 한 번'만 실행되게 합니다.
        if (Keyboard.current != null && Keyboard.current.iKey.wasPressedThisFrame)
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        if (inventoryPanel == null) return;

        // 현재 상태의 반대로 바꿈 (true -> false / false -> true)
        bool isActive = !inventoryPanel.activeSelf;
        inventoryPanel.SetActive(isActive);

        // 3. 인벤토리가 열렸을 때 마우스 커서 설정
        if (isActive)
        {
            Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None; // 커서 자유롭게 이동
        }
        else
        {
            // 인벤토리를 닫으면 다시 게임 화면에 커서 고정 (FPS 등 게임일 경우)
            Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // 기존 함수명 유지 (버튼 이벤트 연결용)
    public void addItem_Button(ItemData newData)
    {
        if(inventoryPanel == null || !inventoryPanel.activeSelf)
        {
            Debug.Log("인벤토리 창이 닫혀 있어 아이템을 추가할 수 없습니다.");
            return;
        }

        if (newData == null) return;
        
        int index = invData.addItem(newData);

        if (index != -1)
        {
            invUI.UpdateSlotUI(index, invData.inventory[index]);
            Debug.Log("추가 성공!");
        }
        else
        {
            Debug.Log("인벤토리가 가득 찼습니다.");
        }
    }
}