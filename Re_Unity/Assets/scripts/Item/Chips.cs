using UnityEngine;

[CreateAssetMenu(fileName = "New Chip", menuName = "Item/Chip")]
public class Chips : ItemData
{
    [SerializeField] public int Hpbonus, StaminaBonus, DamageBonus, MaxammoBonus, FirerateBonus, ReloadBonus;
}
