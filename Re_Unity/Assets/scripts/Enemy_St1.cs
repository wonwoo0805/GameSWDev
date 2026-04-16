using UnityEngine;
using UnityEngine.AI;
public class Enemy_St1 : Box
{

    public enum EnemyState {Patrol, Chase, Search, Attack}
    [Header("Current State")]
    public EnemyState currentState = EnemyState.Patrol;
    [Header("Stats")]
    public float enemyMaxHealth = 100f; // 최대체력
    private float currentHealth; // 현재체력
    public float moveSpeed = 3f; // 이속
    public float detectionRadius = 15f;//감지거리
    public float attackRange = 2f;//공격거리
    public float attackRadius = 1f;//공격범위
    public float attackDamage = 10f;//공격데미지
    public float attackCooldown = 2f;//공속
    public LayerMask playerLayer;

    [Header("Patrol Settings")]
    public float patrolRadius = 10f;
    public float patrolWaitTime = 2f;
    private float patrolTimer;

    [Header("Search Settings")]
    private Vector3 lastKnownPosition; // 플레이어 마지막 위치
    private bool hasLastKnownPos = false;



    //NavMesh라고 유니티 지원 길찾는 ai
    private NavMeshAgent agent;
    //이걸로 플레이어 위치 가져옴
    private Transform playerTransform;
    private float lastAttackTime;

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

        SetState(EnemyState.Patrol);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform == null)
        {
            return;
        }
        DefineState();
        DoStateActions();
    }

    void SetState(EnemyState newState)
    {
        if(currentState == newState)
        {
            return;
        }
        currentState = newState;

        if(newState == EnemyState.Patrol)
        {
            patrolTimer = patrolWaitTime;
            agent.speed = moveSpeed * 0.5f;
        }
        else if(newState == EnemyState.Chase)
        {
            agent.speed = moveSpeed;
            agent.isStopped = false;
        }
    }

    private void DefineState()
    {
        

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);


        if(distanceToPlayer <= attackRange)
        {
            SetState(EnemyState.Attack);
        }
        else if(distanceToPlayer <= detectionRadius)
        {
            SetState(EnemyState.Chase);
        }
        else if (hasLastKnownPos)
        {
            SetState(EnemyState.Search);
        }
        else
        {
            SetState(EnemyState.Patrol);
        }
    }

    void DoStateActions()
    {
        switch (currentState)
        {
            case EnemyState.Patrol: RunPatrol(); break;
            case EnemyState.Chase: RunChase(); break;
            case EnemyState.Search: RunSearch(); break;
            case EnemyState.Attack: RunAttack(); break;
        }
    }

    Vector3 GetRandomPatrolPos()
    {
        Vector3 randomDir = Random.insideUnitSphere * patrolRadius;
        randomDir += transform.position;

        NavMeshHit pt;
        NavMesh.SamplePosition(randomDir, out pt, patrolRadius,1);
        return pt.position;
    }

    void RunPatrol()
    {
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            patrolTimer += Time.deltaTime;
            if(patrolTimer >= patrolWaitTime)
            {
                Vector3 newPos = GetRandomPatrolPos();
                agent.SetDestination(newPos);
                patrolTimer = 0f;
            }
        }
    }

    void RunChase()
    {
        lastKnownPosition = playerTransform.position;
        hasLastKnownPos = true;
        agent.SetDestination(lastKnownPosition);
    }

    void RunSearch()
    {
        agent.SetDestination(lastKnownPosition);

        if(agent.remainingDistance <= agent.stoppingDistance + 0.1f)
        {
            hasLastKnownPos = false;
        }
    }

    void RunAttack()
    {
        agent.isStopped = true;
        transform.LookAt(playerTransform.position);

        if(Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            Attack();
        }
    }

    private void Attack()
    {
        lastAttackTime = Time.time;
        Debug.Log($"{gameObject.name}의 공격");

        Vector3 hitCenter = transform.position + transform.forward * 1.0f;

        Collider[] hitColliders = Physics.OverlapSphere(hitCenter,attackRadius,playerLayer);

        foreach(var hit in hitColliders)
        {
            Debug.Log("플레이어 타격 성공!");
            Player_St1 player = hit.GetComponent<Player_St1>();
            if(player != null)
            {
                player.TakeDamage(attackDamage);
            }
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
        Drop();
        gameObject.SetActive(false);
        currentHealth = enemyMaxHealth;
        SetState(EnemyState.Patrol);
    }
     public override void Drop()
    {
        if(itemTable != null)
        {
            ItemData droppedItem = itemTable.GetRandomItem();
            if(droppedItem.itemPrefab != null)
            {
                Vector3 spawnPos = transform.position + Random.insideUnitSphere * 0.5f;
                Instantiate(droppedItem.itemPrefab,spawnPos,Quaternion.identity);
                    
            }
            Debug.Log($"{droppedItem.name} 드랍");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 hitCenter = transform.position + transform.forward * 1.0f;
        Gizmos.DrawWireSphere(hitCenter,attackRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

    }
}
