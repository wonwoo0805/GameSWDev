using UnityEngine;

[CreateAssetMenu(fileName = "New Chip", menuName = "Item/Chip")]
public class Chips : ItemData
{
    [SerializeField] public int Hpbonus, StaminaBonus, DamageBonus, MaxammoBonus, WeightBonus, AttackBonus, AttackPercentBonus;
    [SerializeField] public float FirerateBonus, ReloadBonus;
}
