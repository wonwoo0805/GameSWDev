using UnityEngine;
using UnityEngine.Events;

public class IntGameEventListener : MonoBehaviour
{
    [SerializeField] private IntGameEvent gameEvent;
    [SerializeField] private UnityEvent<int> response;

    private void OnEnable() => gameEvent?.RegisterListener(this);
    private void OnDisable() => gameEvent?.UnregisterListener(this);
    public void OnEventRaised(int value) => response.Invoke(value);
}
