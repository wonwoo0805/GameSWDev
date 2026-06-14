using UnityEngine;
using TMPro; // TextMeshPro를 사용한다면

public class GoldUIUpdater : MonoBehaviour
{
    public TextMeshProUGUI MainPanelMoneyText;
    public TextMeshProUGUI StorePanelMoneyText;

    void Start()
    {
        // 처음 시작할 때 한 번 업데이트
        UpdateGoldUI();
        
        // InventoryManager의 돈 변경 이벤트에 나를 등록(구독)
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.OnGoldChanged += UpdateGoldUI;
        }
        if (StoreManager.Instance != null)
        {
            StoreManager.Instance.OnGoldChanged += UpdateGoldUI;
        }
        if (StorageManager.Instance != null)
        {
            StorageManager.Instance.OnGoldChanged += UpdateGoldUI;
        }
    }

    void OnDestroy()
    {
        // 오브젝트가 파괴될 때 이벤트 구독 해제 (메모리 누수 방지)
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.OnGoldChanged -= UpdateGoldUI;
        }
    }

    public void UpdateGoldUI()
    {
        Debug.Log("asdfjoweifuvhwe9pbfcdushb");
        if (InventoryManager.Instance != null)
        {
            
            MainPanelMoneyText.text = string.Format("{0:#,###} G", InventoryManager.Instance.invData.totalMoney);
            StorePanelMoneyText.text = string.Format("{0:#,###} G", InventoryManager.Instance.invData.totalMoney);
        }
    }
}