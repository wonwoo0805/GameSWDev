using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Audio.GeneratorInstance;
public class Player_St1 : MonoBehaviour
{
    private CharacterController controller;

    //스텟관련 변수들 모음
    public float playerMaxHealth = 150f;
    private float currentHP;
    public int limitWeight = 100;

    //스텟 보너스 변수들 모음
    public int weightBonus = 0;


    //이동관련 변수들 모음
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    private bool isSprint = false;
    private bool isFiring = false;
    private Vector2 movement;
    private Vector3 jumpVelocity;
    public float finalSpeed = 0f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 0.1f;
    public Transform playerView;
    public FireSystem currentWeapon;
    

    private Vector2 lookInput;
    private float xRotation = 0f;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        walkSpeed = 5f;
        runSpeed = 10f;
        currentHP = playerMaxHealth;

        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked; // 커서 중앙 고정
        Cursor.visible = false; // 커서 안보이게함
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
    }

    public void TakeDamage(float health)
    {
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

        isSprint = context.ReadValueAsButton();
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

        //최종 속도는 isSprint 값에 의해 결정됨
        finalSpeed = isSprint ? runSpeed : walkSpeed; 

        Vector3 moveDirection = transform.right * movement.x + transform.forward * movement.y;
        
        controller.Move(moveDirection * finalSpeed * Time.deltaTime);// deltaTime 사용함으로써 프레임 따라 계산되게 함
        jumpVelocity.y += gravity * Time.deltaTime;
        controller.Move(jumpVelocity * Time.deltaTime);

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


