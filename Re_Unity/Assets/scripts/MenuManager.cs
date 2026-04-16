using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    [Header("UI Panel")]
    public GameObject MenuPanel;

    [Header("UI Toggle feature")]
    public PlayerInput playerInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MenuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnToggleMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ToggleMenu();
        }

        //Debug.Log("Check");
    }

    public void OnCloseMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CloseMenu();
        }

        //Debug.Log("Check");
    }
    public void OnCloseMenu()
    {
        CloseMenu();
    }

    private void ToggleMenu()
    {
        if (MenuPanel == null) return;
        MenuPanel.SetActive(true); // ui active
        playerInput.SwitchCurrentActionMap("UI"); // change action map to ui
        Cursor.lockState = CursorLockMode.None; // change cursor state
        Cursor.visible = true;
    }

    private void CloseMenu()
    {
        MenuPanel.SetActive(false);
        playerInput.SwitchCurrentActionMap("Player");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
