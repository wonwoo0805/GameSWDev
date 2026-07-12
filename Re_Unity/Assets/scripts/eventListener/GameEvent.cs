using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Void Event", menuName = "Game Events/Void Event")]
public class VoidGameEvent : ScriptableObject
{
    private readonly List<VoidGameEventListener> listeners = new List<VoidGameEventListener>();
    public void Raise() { for (int i = listeners.Count - 1; i >= 0; i--) listeners[i].OnEventRaised(); }
    public void RegisterListener(VoidGameEventListener l) => listeners.Add(l);
    public void UnregisterListener(VoidGameEventListener l) => listeners.Remove(l);
}

[CreateAssetMenu(fileName = "New Float Event", menuName = "Game Events/Float Event")]
public class FloatGameEvent : ScriptableObject
{
    private readonly List<FloatGameEventListener> listeners = new List<FloatGameEventListener>();
    public void Raise(float value) { for (int i = listeners.Count - 1; i >= 0; i--) listeners[i].OnEventRaised(value); }
    public void RegisterListener(FloatGameEventListener l) => listeners.Add(l);
    public void UnregisterListener(FloatGameEventListener l) => listeners.Remove(l);
}

[CreateAssetMenu(fileName = "New Int Event", menuName = "Game Events/Int Event")]
public class IntGameEvent : ScriptableObject
{
    private readonly List<IntGameEventListener> listeners = new List<IntGameEventListener>();
    public void Raise(int value) { for (int i = listeners.Count - 1; i >= 0; i--) listeners[i].OnEventRaised(value); }
    public void RegisterListener(IntGameEventListener l) => listeners.Add(l);
    public void UnregisterListener(IntGameEventListener l) => listeners.Remove(l);
}