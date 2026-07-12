using UnityEngine;
using UnityEngine.Events;

public class FloatGameEventListener : MonoBehaviour
{
    [SerializeField] private FloatGameEvent gameEvent;
    [SerializeField] private UnityEvent<float> response; // 유니티 인스펙터에 float 인자값을 받는 [+] 칸을 만듦

    private void OnEnable() => gameEvent?.RegisterListener(this);
    private void OnDisable() => gameEvent?.UnregisterListener(this);
    public void OnEventRaised(float value) => response.Invoke(value);
}
