using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public GameObject settingPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        settingPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
