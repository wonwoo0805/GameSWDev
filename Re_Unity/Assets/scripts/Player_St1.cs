using UnityEngine;
using UnityEngine.InputSystem;
public class Player_St1 : MonoBehaviour
{
    private CharacterController controller;
    

    public float playerMaxHealth = 150f;
    private float currentHP;
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


    public void OnESC(InputValue value)
    {
        //만약 esc 누르면
        if (value.isPressed)
        {
            Cursor.lockState = CursorLockMode.None; // 고정해제
            Cursor.visible = true; // 다시 보이게

            Debug.Log("커서 잠금 해제");
        }
    }

    public void OnInteraction(InputValue value)
    {
        currentWeapon.Interaction();
    }

    public void OnFire(InputValue value)
    {
        isFiring = value.isPressed;
    }

    private void FireCheck()
    {
        if (isFiring && currentWeapon != null)
        {
            currentWeapon.TryShoot();
        }
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && controller.isGrounded)
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
    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    //RunningCheck
    public void OnSprint(InputValue value)
    {
        isSprint = value.isPressed;
    }
    //Actual Moving

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
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
