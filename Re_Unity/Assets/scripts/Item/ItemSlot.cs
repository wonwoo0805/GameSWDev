using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class ItemSlot
{
    public ItemData itemInSlot;
    public int quantity;
    
    public bool isEmpty => itemInSlot == null;

    public ItemSlot(ItemData newItem = null, int count = 0)
    {
        itemInSlot = newItem;
        quantity = count;
    }

    
    
}
