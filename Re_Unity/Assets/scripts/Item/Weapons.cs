using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Item/Weapon")]
public class Weapons : ItemData
{
    [Header("Weapon Stats")]
    [SerializeField] public int damage, maxAmmo, fireRate, reloadSpeed;

    [Header("WeaponOnHand")]
    [SerializeField] private GameObject weaponPrefab;

    [Header("Fire Effect")]
    [SerializeField] private GameObject flamePreFab;
    [SerializeField] private GameObject hitPrefab;
    [SerializeField] private Transform firePoint;

    [Header("Audio Effects")]
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private AudioClip reloadSound;

    public GameObject WeaponPrefab => weaponPrefab;
    public GameObject FlamePrefab => flamePreFab;
    public GameObject HitPrefab => hitPrefab;
    public AudioClip FireSound => fireSound;
    public AudioClip ReloadSound => reloadSound;
    public Transform FirePoint => firePoint;
}
