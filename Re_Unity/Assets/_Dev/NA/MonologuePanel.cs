using System.Collections;
using TMPro;
using UnityEngine;

public class MonologuePanel : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI text;
    public float lineDuration = 3f;

    void Start() { panel.SetActive(false); }

    public void Play(string[] lines)
    {
        if (lines == null || lines.Length == 0) return;
        StopAllCoroutines();
        StartCoroutine(PlayRoutine(lines));
    }

    private IEnumerator PlayRoutine(string[] lines)
    {
        panel.SetActive(true);
        foreach (string line in lines)
        {
            text.text = line;
            yield return new WaitForSeconds(lineDuration);
        }
        panel.SetActive(false);
    }
}