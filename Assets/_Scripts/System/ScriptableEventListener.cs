using UnityEngine;
using UnityEngine.Events;

public class ScriptableEventListener : MonoBehaviour
{
    [SerializeField] private ScriptableEvent _scriptableEvent;
    [SerializeField] private UnityEvent _response;

    private void OnEnable()
    {
        _scriptableEvent.Subscribe(OnEventRaised);
    }

    private void OnEventRaised()
    {
        _response?.Invoke();
    }

    private void OnDisable()
    {
        _scriptableEvent.Unsubscribe(OnEventRaised);
    }
}