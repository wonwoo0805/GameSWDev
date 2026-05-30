using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class SettingManager : MonoBehaviour
{
    [Header("Sensitivity")]
    public Slider sensitivitySlider;
    public TextMeshProUGUI sensitivityText;

    [Header("Volume")]
    public Slider volumeSlider;
    public TextMeshProUGUI volumeText;

    [Header("Button")]
    public UnityEngine.UI.Button applyButton;
    public UnityEngine.UI.Button backButton;

    public GameObject mainPanel;
    public GameObject settingPanel;
    private Player_St1 player;

    private float PlayerVolume = 0;
    private float PlayerSensitivity = 0;

    private void Start()
    {
        player = FindAnyObjectByType<Player_St1>();
        DontDestroyOnLoad(transform.root.gameObject);
        settingPanel.SetActive(false);

        // 슬라이더 초기값 설정
        sensitivitySlider.value = player.mouseSensitivity;
        volumeSlider.value = AudioListener.volume;

        // 슬라이더 값 변경 시 함수 연결
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);

        // 초기 텍스트 설정
        sensitivityText.text = $"Sensitivity: {player.mouseSensitivity:F2}";
        volumeText.text = $"Volume: {(int)(AudioListener.volume * 100)}%";

        applyButton.onClick.AddListener(ApplyVolumeAndSensitivity);
        backButton.onClick.AddListener(BackToMain);
    }

    private void OnSensitivityChanged(float value)
    {
        PlayerSensitivity = value;
        sensitivityText.text = $"Sensitivity: {PlayerSensitivity:F2}";
    }

    private void OnVolumeChanged(float value)
    {
        PlayerVolume = value;
        volumeText.text = $"Volume: {(int)(PlayerVolume * 100)}%";
    }

    private void ApplyVolumeAndSensitivity()
    {
        AudioListener.volume = PlayerVolume;
        player.mouseSensitivity = PlayerSensitivity;
    }

    private void BackToMain()
    {
        mainPanel.SetActive(true);
        settingPanel.SetActive(false);
    }
}