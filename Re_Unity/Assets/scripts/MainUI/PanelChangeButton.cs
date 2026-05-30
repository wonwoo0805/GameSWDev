using UnityEngine;
using UnityEngine.UI;

public class PanelChangeButton : MonoBehaviour
{
    public Button changerButton;
    public GameObject disActivePanel;
    public GameObject activePanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        changerButton.onClick.AddListener(PanelActivation);
        Debug.Log("asdfasdfasdfasdf");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PanelActivation()
    {
        Debug.Log("asdfasdfasdfasdf");
        if (activePanel != null) activePanel.SetActive(true);
        if (disActivePanel != null) disActivePanel.SetActive(false);
    }
}
