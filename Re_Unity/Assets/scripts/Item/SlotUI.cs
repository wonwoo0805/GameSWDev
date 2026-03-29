using UnityEngine;
using UnityEngine.UI;


public class SlotUI : MonoBehaviour
{
    public Image slotImage;    //slot background
    public Image itemImage;


    public void UpdateSlot(ItemSlot item)
    {
        itemImage.sprite = item.itemInSlot.itemDataImage;
        itemImage.enabled = true;
    }

}
