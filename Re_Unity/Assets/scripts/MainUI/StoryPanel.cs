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
    public MonologuePanel monologuePanel;
    public static StoryPanel Instance;
    private string[] pendingMonologue;

    void Start()
    {
        panel.SetActive(false);
        if (closeButton != null)
            closeButton.onClick.AddListener(Hide);
    }
    private void Awake()
    {
        Instance = this;
    }

    public void ShowStory(string text, string[] monologue)
    {
        storyText.text = text;
        pendingMonologue = monologue;
        panel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Hide()
    {
        panel.SetActive(false);
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (monologuePanel != null && pendingMonologue != null)
            monologuePanel.Play(pendingMonologue);
    }
}