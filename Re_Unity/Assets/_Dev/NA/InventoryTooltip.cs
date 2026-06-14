using UnityEngine;

public class InventoryTooltip : ItemExplainPanel
{
    public static InventoryTooltip Instance;
    public Vector3 offset = new Vector3(40f, -40f, 0f);

    void Awake() {     
        if (Instance == null)
        Instance = this; 
        }

    public void ShowAt(ItemData item, Vector3 slotPos) // 인벤토리 위에 뜨게
    {
        ShowDescription(item);
        if (item != null && panel != null)
            panel.transform.position = slotPos + offset;
    }
}