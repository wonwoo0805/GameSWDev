using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    // DDOL 처리된 Canvas 하위의 패널들
    public GameObject inventoryPanel;
    public GameObject storePanel;
    public GameObject settingPanel;
    public GameObject storagePanel;

    void Awake()
    {
        // 아까 교정한 '철통 보안' 싱글톤 로직 적용
        if (Instance != null && Instance != this)
        {
            Destroy(transform.root.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(transform.root.gameObject);
    }

    // 이름을 통해 패널을 제어하는 공용 함수
    public void TogglePanel(string panelName, bool isActive)
    {
        if (panelName == "Inventory" && InventoryManager.Instance != null)
        {
            if(isActive)
            {
                InventoryManager.Instance.ToggleInventory();
            } else
            {
                InventoryManager.Instance.CloseInventory();
            }
        }
        else if (panelName == "Store" && StoreManager.Instance != null)
        {
            if (isActive)
            {
                StoreManager.Instance.OnOpenButtonClick();
            }
            else
            {
                StoreManager.Instance.OnCloseButtonClick();
            }
        }
        else if (panelName == "Setting" && SettingManager.Instance != null)
        {
            if (isActive)
            {
                SettingManager.Instance.settingPanel.SetActive(true);
            }
            else
            {
                SettingManager.Instance.BackToMain();
            }
        }
        else if (panelName == "Storage" && StorageManager.Instance != null)
        {
            if (isActive)
            {
                StorageManager.Instance.OpenStorage();
            }
            else
            {
                StorageManager.Instance.CloseStorage();
            }
        }

    }
}