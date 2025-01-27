using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableEvent",menuName = "Scriptables/ScriptableEvent", order = 0)]
public class ScriptableEvent : ScriptableObject
{
    private event Action OnEventRaise;

    public void Subscribe(Action listener)
    {
        OnEventRaise += listener;
    }
    
    public void Raise()
    {
        OnEventRaise?.Invoke();    
    }
    
    public void Unsubscribe(Action listener)
    {
        OnEventRaise -= listener;
    }
}