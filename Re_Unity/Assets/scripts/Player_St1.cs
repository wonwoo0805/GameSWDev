using UnityEngine;
using UnityEngine.InputSystem;
public class Player_St1 : MonoBehaviour
{
    protected int hp;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    private bool isSprint = false;
    private Vector2 movement;
    public float finalSpeed = 0f;

    public float mouseSensitivity = 0.1f;
    public Transform playerView;

    private Vector2 lookInput;
    private float xRotation = 0f;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        walkSpeed = 5f;
        runSpeed = 10f;
        Cursor.lockState = CursorLockMode.Locked; // 커서 중앙 고정
        Cursor.visible = false; // 커서 안보이게함
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMoving();
        ProcessRotation();
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
    void SetHp(int health)
    {
        hp = health;
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
        //최종 속도는 isSprint 값에 의해 결정됨
        finalSpeed = isSprint ? runSpeed : walkSpeed; 

        Vector3 forward = transform.forward;//앞
        Vector3 right = transform.right;//우측 (둘다 보는 방향 기준임)

        Vector3 moveDirection = (forward * movement.y + right * movement.x).normalized;
        
        transform.position += moveDirection * finalSpeed * Time.deltaTime;// deltaTime 사용함으로써 프레임 따라 계산되게 함
        

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
