using System;
using UnityEngine;

public class ScriptableValue<T> : ScriptableObject
{
    [SerializeField] protected T _value;
    [SerializeField] protected T _defaultValue;
    [SerializeField][Tooltip("Resets value on enable")] protected bool _resetValue;
       
    public virtual T Value
    {
        get => _value;
        set
        {
            _value = value;
            InvokeOnValueChanged();
        }
    }

    public event Action OnValueChanged;

    protected void InvokeOnValueChanged()
    {
        OnValueChanged?.Invoke();
    }

    private void OnEnable()
    {
        if (!_resetValue) return;
        _value = _defaultValue;
    }
}