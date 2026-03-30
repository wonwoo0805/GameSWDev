using UnityEngine;

public class FireSystem : MonoBehaviour
{
    //무기의 속성(장착무기에 따라 바뀌는건 public임)
    public float fireRate = 7.0f;
    public float range = 100f;
    public float damage = 10f;
    private float nextFireTime = 0f;
    //레이캐스트땜에 넣은거
    public Camera playerCamera;
    public LayerMask targetLayer;

    public void TryShoot()
    {
        if(Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + (1f / fireRate);
        }

    }
    public void Shoot()
    {
        RaycastHit hit;
        Debug.Log($"[{Time.time:F2}]발사");
        if(Physics.Raycast(playerCamera.transform.position,playerCamera.transform.forward,out hit, range, targetLayer))
        {
            Debug.Log($"{hit.transform.name}을 맞춤");
            Debug.Log($"맞은 좌표: {hit.point}");
            Debug.DrawLine(playerCamera.transform.position, hit.point,Color.red,0.5f);
        }
        else
        {
            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * range,Color.green,0.5f);
        }

        //TODO:UI에서 발사 애니메이션 재생하게 하기(UI 최초 설정 이후)
        //TODO:적이 맞았는지 판정하기(적 만든 이후)
        
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
