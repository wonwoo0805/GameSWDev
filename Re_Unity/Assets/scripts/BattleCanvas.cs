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
        // ½Ì±ÛÅæ ÆÐÅÏ: ¾ÀÀÌ ³Ñ¾î°¡µµ ÀÌ °´Ã¼°¡ À¯ÁöµÇµµ·Ï ÇÕ´Ï´Ù.
        if (Instance == null)
        {
            Instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;

            DontDestroyOnLoad(gameObject);
            Debug.Log("BattleCanvas »ý¼ºµÊ");
        }
        else
        {
            Debug.Log("BattleCanvas Áßº¹ »ý¼º - ÆÄ±«µÊ");
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
        Debug.Log("OnSceneLoaded µî·ÏµÊ");
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 2)
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
