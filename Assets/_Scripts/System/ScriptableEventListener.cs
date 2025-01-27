using UnityEngine;
using UnityEngine.Events;

public class ScriptableEventListener : MonoBehaviour
{
    [SerializeField] private ScriptableEvent _scriptableEvent;
    [SerializeField] private UnityEvent _response;

    private void Start()
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