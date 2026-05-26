using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger Instance;

    [Header("Settings")]
    public string sceneToLoad;
    public Vector3 nextSpawnPosition; // 다음 씬에서 플레이어가 위치할 좌표

    private void Awake()
    {
        // 싱글톤 패턴: 씬이 넘어가도 이 객체가 유지되도록 합니다.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 버튼에서 호출할 함수
    public void ChangeScene(string targetScene)
    {
        sceneToLoad = targetScene;
        // 예: 특정 좌표를 미리 지정하거나 함수 인자로 받을 수 있습니다.
        StartCoroutine(LoadSceneAsync());
    }

    // 특정 위치 정보를 함께 전달하며 이동할 때 사용하는 오버로딩
    public void ChangeSceneWithSpawn(string targetScene, Vector3 spawnPos)
    {
        sceneToLoad = targetScene;
        nextSpawnPosition = spawnPos;
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        // 비동기 로딩 시작 (전환 속도 최적화)
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);

        // 로딩이 완료될 때까지 대기
        while (!operation.isDone)
        {
            yield return null;
        }

        // 씬 로딩이 완료된 직후 플레이어 위치 설정
        MovePlayerToSpawnPoint();
    }

    private void MovePlayerToSpawnPoint()
    {
        // "Player" 태그를 가진 객체를 찾습니다.
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // 캐릭터 컨트롤러가 있다면 잠시 끄고 이동시켜야 에러가 나지 않습니다.
            player.transform.position = nextSpawnPosition;
            Debug.Log($"{sceneToLoad}로 이동 완료. 스폰 위치: {nextSpawnPosition}");
        }
    }
}