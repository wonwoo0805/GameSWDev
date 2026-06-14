using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Audio.GeneratorInstance;

public class Player_St1 : MonoBehaviour
{
    private CharacterController controller;

    //스텟관련 변수들 모음
    public float playerMaxHealth = 150f;
    public float currentHP;
    public float limitWeight = 100;
    public float maxStamina = 100;
    public float currentStamina;

    //스텟 보너스 변수들 모음
    public float weightBonus = 0;
    public float hpBonus = 0;
    public float staminaBonus = 0;
    public float maxAmmoBonus = 0;
    public float damageBonus = 0;
    public float fireRateBonus = 0;
    public float attackBonus = 0;
    public float attackPercentBonus = 0;
    public float reloadBonus = 0;


    //이동관련 변수들 모음
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    private bool isSprint = false;
    private bool isFiring = false;
    private bool isExhausted = false;
    private Vector2 movement;
    private Vector3 jumpVelocity;
    public float finalSpeed = 0f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 0.1f;
    public Transform playerView;
    public FireSystem currentWeapon;
    public AudioSource audioSource;
    public GuageBar hpBar;
    public GuageBar spBar;
    public GuageBar weightBar;
    
    public float footstepInterval = 0.5f;
    private float footstepTimer;

    private Vector2 lookInput;
    private float xRotation = 0f;
    public AudioClip[] walkSound = new AudioClip[10];

    public static Player_St1 Instance;


    private void Awake()
    {
        //maintain playerInfo while changing scene
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
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        walkSpeed = 5f;
        runSpeed = 10f;
        playerMaxHealth = playerMaxHealth + playerMaxHealth * hpBonus / 100;
        currentHP = playerMaxHealth;
        maxStamina = maxStamina + maxStamina * staminaBonus / 100;
        currentStamina = maxStamina;

        controller = GetComponent<CharacterController>();

        //Cursor.lockState = CursorLockMode.Locked; // 커서 중앙 고정
        //Cursor.visible = false; // 커서 안보이게함

        hpBar.Initialize(playerMaxHealth, currentHP, Color.red);
        spBar.Initialize(maxStamina, currentStamina, Color.green);
        weightBar.Initialize(limitWeight, currentWeapon.inventoryManager.invData.totalWeight, Color.black);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMoving();
        ProcessRotation();
        FireCheck();
        
    }


    public void OnESC(InputAction.CallbackContext context)
    {
        //만약 esc 누르면
        if (context.ReadValueAsButton())
        {
            Cursor.lockState = CursorLockMode.None; // 고정해제
            Cursor.visible = true; // 다시 보이게

            Debug.Log("커서 잠금 해제");
        }
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentWeapon.Reload();
        }
        
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentWeapon.Interaction();
        }
        
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        isFiring = context.ReadValueAsButton();
        
    }

    private void FireCheck()
    {
        if (isFiring && currentWeapon != null)
        {
            currentWeapon.TryShoot();
        }
    }

    public void OnUseHeals(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            currentWeapon.UseItem();
        }
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton() && controller.isGrounded)
        {
            jumpVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                
        }
    }

    public void Die()
    {
        Debug.Log("죽음!");

        InventoryManager.Instance.initInventory();
        currentHP = playerMaxHealth;
        SceneChanger.Instance.ChangeScene("MainLobbyUI");
    }

    public void TakeDamage(float health)
    {
        hpBar.UpdateBar(currentHP);
        currentHP -= health;
        if(currentHP <= 0f)
        {
            Die();
        }
    }
    
    //MovingCheck
    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();

           
    }

    //RunningCheck
    public void OnSprint(InputAction.CallbackContext context)
    {
        
        isSprint = (context.ReadValueAsButton() && (currentStamina > 1) && !isExhausted);
        if(!isSprint && (currentStamina < 30))
        {
            isExhausted = true;
        }
        else
        {
            isExhausted = false;
        }

        return;
    }
    //Actual Moving

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
    private void ProcessMoving()
    {
        
        if (controller.isGrounded && jumpVelocity.y < 0)
        {
            jumpVelocity.y = -2f;
        }

        
        finalSpeed = isSprint ? runSpeed : walkSpeed; 

    
        Vector3 moveDirection = transform.right * movement.x + transform.forward * movement.y;
        Vector3 finalMovement = moveDirection * finalSpeed; 

    
        jumpVelocity.y += gravity * Time.deltaTime;
        finalMovement.y = jumpVelocity.y; 

    
        controller.Move(finalMovement * Time.deltaTime);

        if (isSprint)
        {
            if (currentStamina > 0)
                currentStamina -= 0.05f;
        }
        else
        {
            if (currentStamina < maxStamina)
                currentStamina += 0.03f;
        } 
        spBar.UpdateBar(currentStamina);

        bool isMoving = controller.isGrounded && (movement.sqrMagnitude > 0.01f);
        if(walkSound != null && isMoving == true)
        {
            
            footstepTimer += Time.deltaTime;
            
            if (footstepTimer >= footstepInterval)
            {
                audioSource.PlayOneShot(walkSound[Random.Range(0, walkSound.Length)]);
                footstepTimer = 0f;
            }

        }
        else
        {
            if (movement.sqrMagnitude <= 0.01f)
            {
                footstepTimer = footstepInterval; 
            } 
        }
    }

    private void ProcessRotation()
    {
        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerView.localRotation = Quaternion.Euler(xRotation,0f,0f);
    }

    
}


