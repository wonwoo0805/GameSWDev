using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipDismisser : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData) //인벤토리 팝업 끄는용
    {
        if (InventoryTooltip.Instance != null)
            InventoryTooltip.Instance.ShowDescription(null);
    }
}