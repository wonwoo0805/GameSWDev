using UnityEngine;
using UnityEngine.AI;
public class Enemy_St1 : MonoBehaviour
{
    //적 스탯들
    //적 체력
    public float enemyMaxHealth = 100f;
    private float currentHealth;
    //이동속도(NavMesh Agent 스피드에 덧씌워질거임)
    public float moveSpeed = 3f;
    //플레이어 발견범위
    public float detectionRadius = 15f;
    //NavMesh라고 유니티 지원 길찾는 ai
    private NavMeshAgent agent;
    //이걸로 플레이어 위치 가져옴
    private Transform playerTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = enemyMaxHealth;
        //NavMeshAgent 컴포넌트 가져와서 초기화
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        //player 객체를 태그로 찾아서 가져옴 -> 다시말해 씬당 플레이어가 둘이면 문제생길수도 있단소리임
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            playerTransform = player.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        TrackingPlayer();
    }

    private void TrackingPlayer()
    {
        if(playerTransform == null)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if(distanceToPlayer <= detectionRadius)
        {
            agent.SetDestination(playerTransform.position);//플레이어의 위치를 목적지로 정함
        }
        else
        {
            agent.ResetPath();//범위 밖으로 나가있으면 멈춤
        }
    }
    public void TakeDamage(float damageAmout)
    {
        currentHealth -= damageAmout;
        Debug.Log($"{gameObject.name} 체력 : {currentHealth}");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("좀비 사망!");
        Destroy(gameObject);
    }
}
