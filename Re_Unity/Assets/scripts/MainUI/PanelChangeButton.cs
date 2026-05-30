using UnityEngine;
using UnityEngine.UI;

public class PanelChangeButton : MonoBehaviour
{
    public Button changerButton;
    public string panelToDeactive;
    public string panelToActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        changerButton.onClick.AddListener(() => {
            // 매니저에게 이름으로 명령 전달
            if (!string.IsNullOrEmpty(panelToActive))
                UIManager.Instance.TogglePanel(panelToActive, true);

            if (!string.IsNullOrEmpty(panelToDeactive))
                UIManager.Instance.TogglePanel(panelToDeactive, false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
