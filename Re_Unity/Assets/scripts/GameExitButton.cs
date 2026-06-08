using UnityEngine;
using UnityEngine.UI;

public class GameExitButton : MonoBehaviour
{
    public Button exitButton;

    void Start()
    {
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame);
        }
    }

    public void ExitGame()
    {
        Debug.Log("게임 종료 버튼 클릭됨");

        Application.Quit();

        UnityEditor.EditorApplication.isPlaying = false;

    }
}