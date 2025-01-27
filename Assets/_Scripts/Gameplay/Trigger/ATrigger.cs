using UnityEngine;

public abstract class ATrigger : MonoBehaviour
{
    [SerializeField] protected ScriptableEvent _triggerEvent;
    
    protected bool _isTriggered;
    
    private void OnEnable()
    {
        _isTriggered = false;
    }
}
