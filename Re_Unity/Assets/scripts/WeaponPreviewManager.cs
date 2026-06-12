using UnityEngine;
using System.Collections.Generic;

public class WeaponPreviewManager : MonoBehaviour
{
    public static WeaponPreviewManager Instance;
    public Transform spawnPoint;
    public Camera previewCamera;

    private GameObject currentPreview;
    private Animator currentAnimator;

    public GameObject CurrentPreview => currentPreview;

    private void Awake()
    {
        // �̱��� ����: ���� �Ѿ�� �� ��ü�� �����ǵ��� �մϴ�.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("SceneChanger ������");
        }
        else
        {
            Debug.Log("SceneChanger �ߺ� ���� - �ı���");
            Destroy(transform.root.gameObject);
        }
    }

    public void ChangeItemPreview(ItemData data)
    {
        if (data == null || data.itemPrefab == null)
        {
            Debug.Log("cancel");
            return;
        }
        if (currentPreview != null) Destroy(currentPreview);

        if(data is Weapons currentWeapon)
        {
            ChangeWeaponPreview(currentWeapon);
        }

    }

    public void ChangeWeaponPreview(Weapons currentWeapon)
    {
        if(currentPreview != null) Destroy(currentPreview);
        
        currentPreview = Instantiate(currentWeapon.WeaponPrefab, spawnPoint.position,spawnPoint.rotation);
        currentAnimator = currentPreview.GetComponent<Animator>();

        PrefareForUI(currentPreview);

        SetLayerRecursively(currentPreview,LayerMask.NameToLayer("UI_3D"));

    }

    public void PlayFireAnimation()
    {
        if(currentAnimator != null)
        {
            currentAnimator.Play("Shoot",0,0f);
        }
    }

    private void PrefareForUI(GameObject obj)
    {
        if(obj.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.isKinematic = true;
        }
        foreach(var col in obj.GetComponentsInChildren<Collider>())
        {
            col.enabled = false;
        }
    }

    public void PlayReloadAnimation()
    {
        if(currentAnimator != null)
        {
            currentAnimator.Play("Reload",0,0f);
        }
    }

    private void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;
        foreach(Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
