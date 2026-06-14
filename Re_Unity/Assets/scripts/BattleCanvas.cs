using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BattleCanvas : MonoBehaviour
{
    public static BattleCanvas Instance;
    public GameObject battelPanel;
    public PlayerInput playerInput;
    private bool isFiring;

    private void Awake()
    {
        // �̱��� ����: ���� �Ѿ�� �� ��ü�� �����ǵ��� �մϴ�.
        if (Instance == null)
        {
            Instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;

            DontDestroyOnLoad(gameObject);
            Debug.Log("BattleCanvas ������");
        }
        else
        {
            Debug.Log("BattleCanvas �ߺ� ���� - �ı���");
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        battelPanel.SetActive(true);
        //DontDestroyOnLoad(transform.root.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log("OnSceneLoaded ��ϵ�");
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            battelPanel.SetActive(false);
        } else
        {
            battelPanel.SetActive(true);
        }

    }

    public void Activate(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            battelPanel.SetActive(true);
        }

        
    }
    public void Deactivate(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            battelPanel.SetActive(false);
        }
        
    }

    
}
