using UnityEngine;
using UnityEngine.InputSystem;

public class BattleCanvas : MonoBehaviour
{
    public GameObject battelPanel;
    public PlayerInput playerInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        battelPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
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
