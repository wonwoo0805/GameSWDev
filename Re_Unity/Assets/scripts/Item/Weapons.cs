using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Item/Weapon")]
public class Weapons : ItemData
{
    
    [SerializeField] public int damage, maxAmmo, fireRate, reloadSpeed;
}
