using UnityEngine;
using UnityEngine.UI;

public class SceneChangeButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Button changerButton;
    public string targetScene;

    private void Start()
    {
        changerButton.onClick.AddListener(() => SceneChanger.Instance.ChangeScene(targetScene));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
