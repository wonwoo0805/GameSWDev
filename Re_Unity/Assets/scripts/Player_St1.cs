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
    }

    // Update is called once per frame
    void Update()
    {
        ProcessMoving();
        ProcessRotation();
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
        finalSpeed = isSprint ? runSpeed : walkSpeed; 
        float xOffset = movement.x * finalSpeed * Time.deltaTime;//xAxis movement
        float zOffset = movement.y * finalSpeed * Time.deltaTime;//zAxis movement
        transform.localPosition += new Vector3(xOffset, 0f, zOffset);

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
