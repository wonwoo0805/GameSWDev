using Mono.Cecil;
using UnityEngine;

public class ItemData : ScriptableObject
{
    //define rarity (#define)
    public const int COMMON = 1, RARE = 2, EPIC = 3, UNIQUE = 4, LEGENDARY = 5;

    //define string type
    protected string itemName, itemType;

    //define int type
    protected int money, weight;
    
}
