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
       

        Application.Quit();

        //UnityEditor.EditorApplication.isPlaying = false;

    }
}