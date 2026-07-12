using UnityEngine;
using UnityEngine.Events;

public class VoidGameEventListener : MonoBehaviour
{
    [SerializeField] private VoidGameEvent gameEvent;
    [SerializeField] private UnityEvent response;

    private void OnEnable() => gameEvent?.RegisterListener(this);
    private void OnDisable() => gameEvent?.UnregisterListener(this);
    public void OnEventRaised() => response.Invoke();
}
