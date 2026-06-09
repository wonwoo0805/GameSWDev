using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class FireSystem : MonoBehaviour
{
    //무기의 속성(장착무기에 따라 바뀌는건 public임)
    public float fireRate = 7.0f;
    public float range = 100f;
    public float damage = 10f;
    public float reloadSpeed = 3f;
    public int maxAmmo = 30;
    private float nextFireTime = 0f;
    private float interactionRange = 4f;

    public InventoryManager inventoryManager;
    //레이캐스트땜에 넣은거
    public Player_St1 player;
    //플레이어의 여러 스텟정보를 가져오기 위함
    public Camera playerCamera;
    public LayerMask targetLayer;
    public LayerMask boxLayer;

    public WeaponPreviewManager wpm;
    public AudioSource audioSource;
    private Transform currentFirePoint;

    public ItemData currentItem;
    public Weapons currentWeapon;
    public Uses currentUse;

    private void Awake()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }

    public void TryShoot()
    {
        if(Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + (1f / (fireRate + fireRate * player.fireRateBonus / 100 ));
        }
    }
    public void Shoot()
    {
        if (currentWeapon != null)
        {
            currentFirePoint = wpm.CurrentPreview.transform.Find("FirePoint");
            // 발사음 재생 (AudioSource가 필요합니다)
            if (currentWeapon.FireSound != null)
            {
                audioSource.PlayOneShot(currentWeapon.FireSound);
            }

            // 총구 화염(MuzzleFlash) 생성
            if (currentWeapon.FlamePrefab != null)
            {

                // 총구 위치(muzzlePoint)가 있다면 거기서, 없다면 카메라 앞에서 생성
                // 생성함과 동시에 0.5초 뒤 삭제 예약
                Destroy(Instantiate(currentWeapon.FlamePrefab, currentFirePoint.position, currentFirePoint.rotation), 0.2f);
            }
        }

        RaycastHit hit;
        Debug.Log($"[{Time.time:F2}]발사");
        if(Physics.Raycast(playerCamera.transform.position,playerCamera.transform.forward,out hit, range, targetLayer))
        {
            Debug.Log($"{hit.transform.name}을 맞춤");
            Debug.Log($"맞은 좌표: {hit.point}");
            Debug.DrawLine(playerCamera.transform.position, hit.point,Color.red,0.5f);
            Enemy_St1 enemy = hit.collider.GetComponentInParent<Enemy_St1>();//맞는 콜라이더는 에너미 하위객체임 그래서 부모한테 붙어있는 스크립트 가져오기
            if(enemy != null)
            {
                enemy.TakeDamage(((damage + player.attackBonus) * (1 + player.attackPercentBonus / 100)) * (1 + player.damageBonus / 100));

            }
        }
        else
        {
            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * range,Color.green,0.5f);
        }
        //TODO:UI에서 발사 애니메이션 재생하게 하기(UI 최초 설정 이후)
        wpm.PlayFireAnimation();
    }

    

    public void Interaction()
    {
        RaycastHit hit;
        Debug.Log($"[{Time.time:F2}]상호작용");
        if(Physics.Raycast(playerCamera.transform.position,playerCamera.transform.forward,out hit, interactionRange, boxLayer))
        {
            if(hit.collider.TryGetComponent(out Box targetBox))
            {
                Debug.Log("상자");
                targetBox.Drop();
            }
            else if(hit.collider.TryGetComponent(out ItemObject droppedItem))
            {
                

                Debug.Log($"{droppedItem.itemData.name} 획득");
                int resultIndex = inventoryManager.addItem_Button(droppedItem.itemData); 
                if (resultIndex != -1)
                {
                    Destroy(hit.collider.gameObject);
                }
                else
                {
                    Debug.Log("가방 가득 참");
                }

            }
            else if (hit.collider.TryGetComponent(out StoryNote note)) //내가넣음
            {
                StoryPanel.Instance.ShowStory(note.storyText, note.monologue);
            }

        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log("OnSceneLoaded 등록됨");
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentItem = inventoryManager.GetEquippedItem(ItemType.Weapon);
        currentWeapon = currentItem as Weapons;
        UpdateWeaponStat(currentWeapon);
        wpm.ChangeItemPreview(currentItem);
    }

    void UpdateWeaponStat(Weapons newWeapon)
    {
        damage = newWeapon.damage;
        fireRate = newWeapon.fireRate;
        maxAmmo = newWeapon.maxAmmo;
        reloadSpeed = newWeapon.reloadSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
