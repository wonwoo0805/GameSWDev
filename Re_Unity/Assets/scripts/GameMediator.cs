using UnityEngine;

public class GameMediator : MonoBehaviour
{
    public static GameMediator Instance { get; private set; }

    [Header("Game_System_Flow")]
    [SerializeField] private GameDirector gameDirector;
    [SerializeField] private SceneChanger sceneChanger;

    [Header("Player_System")]
    [SerializeField] private Player_St1 player_St1;
    [SerializeField] private FireSystem fireSystem;

    [Header("Inventory_System")]
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private StoreManager storeManager;
    [SerializeField] private StorageManager storageManager;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            InitSystem();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitSystem()
    {   /*
        player_St1.Init(this);

        inventoryManager.Init(this);
        storeManager.Init(this);
        storageManager.Init(this);

        fireSystem.Init(this);
        gameDirector.Init(this);
        sceneChanger.Init(this);
        */
    }
}
