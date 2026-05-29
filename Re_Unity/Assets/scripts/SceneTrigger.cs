using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    public string targetScene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            SceneChanger.Instance.ChangeScene(targetScene);
    }
}
