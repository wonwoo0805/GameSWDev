using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Item/Chip")]
public class Chips : ItemData
{
    [SerializeField] public int Hpbonus, StaminaBonus, DamageBonus, MaxammoBonus, FirerateBonus, ReloadBonus;
}
