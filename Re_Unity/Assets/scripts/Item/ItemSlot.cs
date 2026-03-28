using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class ItemSlot : ScriptableObject
{
    public ItemData itemInSlot;
    public int quantity;
    
    public bool isEmpty => itemInSlot == null;

    public ItemSlot()
    {
        itemInSlot = null;
        quantity = 0;
    }

    
    
}
