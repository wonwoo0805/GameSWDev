using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryPanel : MonoBehaviour
{
    [Header("UI")]
    public GameObject panel;
    public TextMeshProUGUI storyText;
    public Button closeButton;

    void Start()
    {
        panel.SetActive(false);
        if (closeButton != null)
            closeButton.onClick.AddListener(Hide);
    }

    public void ShowStory(string text)
    {
        storyText.text = text;
        panel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Hide()
    {
        panel.SetActive(false);

        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}