using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger Instance;
    private Player_St1 player;

    [Header("Settings")]
    //public string sceneToLoad;
    public Vector3 nextSpawnPosition; // 다음 씬에서 플레이어가 위치할 좌표

    private void Awake()
    {
        // 싱글톤 패턴: 씬이 넘어가도 이 객체가 유지되도록 합니다.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("SceneChanger 생성됨");
        }
        else
        {
            Debug.Log("SceneChanger 중복 생성 - 파괴됨");
            Destroy(gameObject);
        }
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
        HandleAudioListeners();

        Player_St1 player = FindAnyObjectByType<Player_St1>();
        if (player == null) return;

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None; // 고정해제
            UnityEngine.Cursor.visible = true; // 다시 보이게
        }
        else
        {
            StartCoroutine(MovePlayerToSpawnPoint());
            UnityEngine.Cursor.lockState = CursorLockMode.Locked; // 커서 중앙 고정
            UnityEngine.Cursor.visible = false; // 커서 안보이게함
        }
    }


    private void HandleAudioListeners()
    {
        AudioListener[] listeners = FindObjectsByType<AudioListener>(FindObjectsSortMode.None);

        if (listeners.Length <= 1) return;

        // 메인 카메라의 AudioListener 하나만 남기고 나머지 비활성화
        bool kept = false;
        foreach (AudioListener listener in listeners)
        {
            if (!kept && listener.gameObject.CompareTag("MainCamera"))
            {
                kept = true; // 메인 카메라 것은 유지
            }
            else
            {
                listener.enabled = false; // 나머지는 비활성화
                Debug.Log($"AudioListener 비활성화: {listener.gameObject.name}");
            }
        }
    }

    // 버튼에서 호출할 함수
    public void ChangeScene(string targetScene)
    {
        //sceneToLoad = targetScene;
        // 예: 특정 좌표를 미리 지정하거나 함수 인자로 받을 수 있습니다.
        StartCoroutine(LoadSceneAsync(targetScene));
    }

    private IEnumerator LoadSceneAsync(string targetScene)
    {
        // 비동기 로딩 시작 (전환 속도 최적화)
        AsyncOperation operation = SceneManager.LoadSceneAsync(targetScene);

        // 로딩이 완료될 때까지 대기
        while (!operation.isDone)
        {
            yield return null;
        }
        Debug.Log("ewqrwdfweqfdasvfd");
        // 씬 로딩이 완료된 직후 플레이어 위치 설정
        
    }

    private IEnumerator MovePlayerToSpawnPoint()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log($"현재 씬: {SceneManager.GetActiveScene().name}");
        // find player, spawnpoint and set spawnPosition
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        GameObject spawnPoint = GameObject.Find("SpawnPoint");

        if (spawnPoint)
        {
            nextSpawnPosition = spawnPoint.transform.position;
            Debug.Log("success");
        }
        else
        {
            Debug.Log("asdfasdfasdsf");
        }

        if (player != null)
        {
            // 캐릭터 컨트롤러가 있다면 잠시 끄고 이동시켜야 에러가 나지 않습니다.
            var controller = player.GetComponent<CharacterController>();
            if (controller != null)
                controller.enabled = false;

            player.transform.position = nextSpawnPosition;
            Debug.Log(nextSpawnPosition);

            if (controller != null)
                controller.enabled = true;
        }
        //Debug.Log($"{sceneToLoad}로 이동 완료. 스폰 위치: {nextSpawnPosition}");
    }
    
}